#!/bin/bash
# Install on osx
if [ "$TRAVIS_OS_NAME" = "osx" ]
then
    brew update
    brew install libpcap
fi

# Install on linux
if [ "$TRAVIS_OS_NAME" = "linux" ]
then
  sudo apt-get update
  sudo apt-get -qy install libpcap-dev
fi

# Install on windows
if [ "$TRAVIS_OS_NAME" = "windows" ]
then
    # WinPcap does not actually work on Travis, but we need the rpcapd.exe file
    # See https://github.com/nmap/nmap/issues/1329
    choco install -y winpcap
    cp '/c/Program Files (x86)/WinPcap/rpcapd.exe' $TMP
    # winpcap does not work on travis ci - so install npcap (package is unlisted -> version)
    choco install -y npcap --version 0.86
    mkdir -p '/c/Program Files (x86)/WinPcap'
    cp $TMP/rpcapd.exe '/c/Program Files (x86)/WinPcap'
fi
