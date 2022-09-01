#!/usr/bin/env zsh

# Optional, may give more information
#export NSZombieEnabled=YES
#export NSDebugEnabled=YES

# Settings
APP=ICrashYouCry
CONFIG=Debug

if [[ "$(arch)" == "i386" ]]; then
	RUNTIME=osx-x64
else
	RUNTIME=osx-arm64
fi

# Defaults
TARGET="bin/$CONFIG/net6.0-macos/$RUNTIME/$APP.app/Contents/MacOS/$APP"

xcrun lldb "$TARGET" \
  -o "run" `# start debugging automatically` \
  -o "expr -- @import Foundation" `# make lldb objc capable` \
  -o "expr -- @import AppKit" \
  -o "loadsymbols" `# load symbols for dotnet` \
  -o "process handle --stop false SIGUSR1 SIGUSR2" `# dotnet sends these signals to itself ` \
  -o "continue"
