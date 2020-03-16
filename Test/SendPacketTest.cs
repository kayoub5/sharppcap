using NUnit.Framework;
using PacketDotNet;
using SharpPcap;
using SharpPcap.LibPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using static Test.TestHelper;
using static System.TimeSpan;
using System.Threading;

namespace Test
{
    [TestFixture]
    [NonParallelizable]
    [Category("SendPacket")]
    public class SendPacketTest
    {
        private const string Filter = "ether proto 0x1234";

        [Test]
        public void TestSendPacketTest()
        {
            var packet = EthernetPacket.RandomPacket();
            packet.Type = (EthernetType)0x1234;
            var received = RunCapture(Filter, (device) =>
            {
                device.SendPacket(packet);
            });
            Assert.That(received, Has.Count.EqualTo(1));
            CollectionAssert.AreEquivalent(packet.Bytes, received[0].Data);
        }

        [Test]
        public void TestPcapDeviceGetSequence()
        {
            CheckReceive(GetPcapDevice().Interface);

            var loopInterface = LibPcapLiveDeviceList.Instance.FirstOrDefault(ni => ni.Loopback)?.Interface;
            CheckReceive(loopInterface);

            var anyInterface = LibPcapLiveDeviceList.Instance.FirstOrDefault(ni => ni.Name == "any")?.Interface;
            CheckReceive(anyInterface);
        }

        private void CheckReceive(PcapInterface pcapInterface)
        {
            if (pcapInterface == null)
            {
                return;
            }
            var device = new LibPcapLiveDevice(pcapInterface);
            Console.WriteLine("Using " + device);
            device.Open(DeviceMode.Promiscuous, 0);
            Thread.Sleep(5000);
            foreach (var p in PcapDevice.GetSequence(device, false))
            {
                Console.WriteLine(p);
            }
            device.Close();
        }

        [SetUp]
        public void SetUp()
        {
            TestHelper.ConfirmIdleState();
        }

        [TearDown]
        public void Cleanup()
        {
            TestHelper.ConfirmIdleState();
        }
    }
}

