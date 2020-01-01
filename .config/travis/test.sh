#!/bin/bash
# Test on osx
if [ "$TRAVIS_OS_NAME" = "osx" ]
then
  sudo dotnet test -c Release -f netcoreapp2.1
fi

# Test on linux
if [ "$TRAVIS_OS_NAME" = "linux" ]
then
  sudo dotnet test -c Release -f netcoreapp2.1
fi

# Test on windows
if [ "$TRAVIS_OS_NAME" = "windows" ]
then
    dotnet test -c Release
fi