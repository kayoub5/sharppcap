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
    ls "C:\Program Files (x86)"
    ls "C:\Program Files"
    ls "C:\Program Files (x86)\WinPcap"
    ls "C:\Program Files\WinPcap" 
    for pf in "C:\Program Files (x86)" "C:\Program Files"
    do
        if [ -d "$pf\WinPcap" ]; then
            WINPCAP_DIR="$pf\WinPcap"
        fi
    done
    cp "$WINPCAP_DIR\rpcapd.exe" $TMP
    # winpcap does not work on travis ci - so install npcap (package is unlisted -> version)
    choco install -y npcap --version 0.86
    mkdir -p "$WINPCAP_DIR"
    cp $TMP/rpcapd.exe "$WINPCAP_DIR\rpcapd.exe"
fi
