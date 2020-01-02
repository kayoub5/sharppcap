using NUnit.Framework;
using PacketDotNet;
using System.Net;
using System.Net.NetworkInformation;
using static Test.TestHelper;

namespace Test
{
    [TestFixture]
    [NonParallelizable]
    [Category("SendPacket")]
    public class SendPacketTest
    {
        private const string Filter = "udp port 9999";

        [Test]
        public void TestSendPacketTest()
        {
            var eth = EthernetPacket.RandomPacket();
            eth.DestinationHardwareAddress = PhysicalAddress.Parse("FFFFFFFFFFFF");
            var ip = IPv4Packet.RandomPacket();
            ip.DestinationAddress = IPAddress.Parse("255.255.255.255");
            var udp = UdpPacket.RandomPacket();
            udp.DestinationPort = 9999;
            eth.PayloadPacket = ip;
            ip.PayloadPacket = udp;
            udp.PayloadData = new byte[32];
            var received = RunCapture(Filter, (device) =>
            {
                device.SendPacket(eth);
            });
            Assert.That(received, Has.Count.EqualTo(1));
            CollectionAssert.AreEquivalent(eth.Bytes, received[0].Data);
        }
    }
}
