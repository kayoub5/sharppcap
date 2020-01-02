# CI TODO list

## Linux

linux can't send packets (Travis, CircleCI & AppVeyor)
at least when you make StartCapture on the same adapter nothing is received
possibly related to https://github.com/the-tcpdump-group/libpcap/issues/400

tests for linux have to run using
```
sudo dotnet test -f netcoreapp2.1 --filter TestCategory!=SendPacket
```

## Npcap & rpcapd

rpcapd is broken in Npcap, Npcap is used in Travis
See https://github.com/nmap/nmap/issues/1329

tests for linux have to run using
```
sudo dotnet test -f netcoreapp2.1 --filter TestCategory!=RemotePcap
```

## Npcap OEM installer

latest version of Npcap available as silent installer is 0.86
see https://github.com/nmap/nmap/issues/900
scapy is using an OEM installer, possible to get one from https://nmap.org/npcap/oem/internal.html
sample config https://github.com/secdev/scapy/blob/master/.appveyor.yml#L6

## Configure artifacts path and test resutls path

## Configure coverage