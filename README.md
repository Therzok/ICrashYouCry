# Coredump analysis workshop for dotnet

The implementation contains a macOS application that has a few buttons that cause it to crash.

`coredebug.sh` is used to attach to a coredump.
`livedebug.sh` is used to attach to a live instance of VSMac.

# Setup

```zsh
dotnet tool install -g dotnet-sos dotnet-symbol
dotnet-sos install
```

Documentation links:
* https://learn.microsoft.com/en-us/dotnet/core/diagnostics/sos-debugging-extension
* https://learn.microsoft.com/en-us/dotnet/core/diagnostics/debug-stackoverflow

```zsh
dotnet build
./livedebug.sh
```

This boots up an instance of the app in `lldb`. You may need to set the SOS symbolserver.
