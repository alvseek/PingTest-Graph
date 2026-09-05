# PingTest Graph

A lightweight ping monitoring widget for Windows. See your network latency at a glance with a real-time color-coded graph overlay.

## Background

I built this back in 2018 because I always wanted a small, unobtrusive ping monitor that sits on my desktop. Something I could glance at while gaming, working, or troubleshooting network issues - without opening a terminal or a full-blown monitoring suite.

I released it for free with a donation option. It never got popular enough to receive any donations, but it served me well over the years. After 8 years of sitting in a private repo, I decided to open-source it. If anyone finds it useful and wants features or updates, I'm happy to revisit it.

## Features

- **Tiny overlay widget** - A compact, borderless window (~62x61px) that stays out of your way
- **Real-time ping graph** - Line chart showing your last 10 ping measurements with auto-scrolling
- **Color-coded latency** - Instantly see your connection quality:
  - Green: < 100ms (excellent)
  - Yellow: 100-300ms (good)
  - Orange: 300-1000ms (acceptable)
  - Red: > 1000ms (poor)
  - Purple: timeout / error
- **System tray icon** - Tray icon color reflects current ping status
- **Click-through mode** - Make the window completely transparent to mouse input
- **Adjustable transparency** - Set opacity from 5% to 100%
- **Always-on-top** - Keep the widget above all other windows
- **Draggable** - Click and drag to position anywhere on screen
- **Quick positioning** - Snap to screen corners, edges, or center via right-click menu
- **Preset DNS targets** - Google DNS (8.8.8.8), Cloudflare (1.1.1.1), OpenDNS, or enter a custom IP
- **Run at startup** - Optional Windows startup registration
- **Single instance** - Mutex lock prevents running multiple copies

## Requirements

- **Windows** (tested on Windows 11, originally built on Windows 10)
- **.NET Framework 4.8** (pre-installed on Windows 10 version 1903 and later, and on all Windows 11)

## Building from Source

1. Open `PingTest.sln` in Visual Studio 2015 or later
2. Build the solution (Ctrl+Shift+B)
3. Run from `PingTest/bin/Debug/PingTest Graph.exe` or `PingTest/bin/Release/PingTest Graph.exe`

Alternatively, build from the command line:

```
msbuild PingTest.sln /p:Configuration=Release
```

No NuGet packages or external dependencies required - everything uses built-in .NET Framework libraries.

## Usage

1. Launch the application
2. Right-click the widget to access the context menu
3. Go to **Settings** to configure:
   - Target IP address
   - Window transparency
   - Always-on-top behavior
   - Run at startup
4. Right-click > **Position** to snap the widget to a screen corner or edge
5. Right-click > **Clickable/Unclickable** to toggle click-through mode

## Tech Stack

- C# / .NET Framework 4.8
- Windows Forms
- System.Windows.Forms.DataVisualization (charting)
- Win32 API interop for window transparency effects

## License

Licensed under the Apache License, Version 2.0 - see [LICENSE](LICENSE) and [NOTICE](NOTICE).
You are free to use, modify and redistribute it, including commercially, as long as the
notice is kept.

Copyright 2018 Alviandi Widiasto - [www.indonesiamadjoe.com](http://www.indonesiamadjoe.com)
