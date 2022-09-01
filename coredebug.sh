#!/usr/bin/env zsh

# Optional, may give more information
#export NSZombieEnabled=YES
#export NSDebugEnabled=YES

LAST_CORE=$(find $TMPDIR -iname "coredump*" -xdev -type f -print0 | xargs -0 stat -f "%m %N" | grep -v "json" | sort -rn | head -n 1 | cut -d' ' -f 2)

echo "Attaching to coredump: $LAST_CORE"

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

xcrun lldb -c "$LAST_CORE" \
  "$TARGET" \
  -o "expr -- @import Foundation" `# objc capable` \
  -o "expr -- @import AppKit" \
  -o "loadsymbols" `# load dotnet symbols`
