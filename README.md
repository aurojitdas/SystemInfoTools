# SystemInfoTools

![C#](https://img.shields.io/badge/C%23-10.0-brightgreen)
![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.7.2-purple)
![Windows Forms](https://img.shields.io/badge/Windows%20Forms-UI-blue)
![System.Management](https://img.shields.io/badge/System.Management-WMI-orange)
![Multithreaded](https://img.shields.io/badge/Architecture-Multithreaded-red)
![Version](https://img.shields.io/badge/Version-0.4.0-yellow)

## 📋 Overview

SystemInfoTools is a Windows desktop application that provides real-time hardware monitoring and detailed system information. It offers a comprehensive view of your system's CPU, RAM, operating system, and network information through an easy-to-use interface with live-updating metrics.

## ✨ Features

- **Real-time System Monitoring**
  - CPU utilization with visual progress bar
  - RAM usage with visual progress bar
  - Available memory display
  - Automatic data refresh

- **Detailed System Information**
  - **CPU**: Name, architecture, maximum frequency, current usage
  - **RAM**: Total memory, available memory, memory type, usage percentage
  - **OS**: Windows version, architecture type, installation date, system uptime
  - **Network**: Adapter name, connection status, SSID, signal strength, speed

## 🔧 Technical Implementation

### Architecture

The application is built using a multi-threaded approach:

- **Main UI Thread**: Handles the Windows Forms user interface
- **CPU Monitor Thread**: Continuously monitors CPU performance metrics
- **RAM Monitor Thread**: Tracks memory usage and availability
- **System Info Thread**: Retrieves detailed system information

### Key Components

- **Counters.cs**: Core class that interfaces with Windows performance counters and WMI to gather system metrics
- **Delegators.cs**: Manages the thread-safe updating of UI components with system information
- **Interface.cs**: Defines the user interface and control layout
- **Program.cs**: Main entry point and thread management

### Technical Highlights

- Uses Windows Management Instrumentation (WMI) to access low-level system information
- Implements real-time performance counters through the `System.Diagnostics` namespace
- Employs multi-threading for responsive UI while performing background monitoring
- Custom network adapter information retrieval using command-line utilities
- Thread-safe UI updates using delegates

## 🚀 Getting Started

### Prerequisites

- Windows 7 or later
- .NET Framework 4.7.2 or later
- Administrative privileges (for accessing certain system metrics)

### Installation

1. **Download the latest release**:
   - Download the ZIP file from the [Releases](https://github.com/yourusername/SystemInfoTools/releases) page

2. **Extract and Run**:
   - Extract the ZIP file to your desired location
   - Run `SystemInfoTools.exe`
   - No installation required!

### Building from Source

1. **Clone the repository**:
   ```
   git clone https://github.com/yourusername/SystemInfoTools.git
   ```

2. **Open in Visual Studio**:
   - Open the solution file `SystemInfoTools.sln` in Visual Studio
   - Ensure you have the .NET Desktop Development workload installed

3. **Build the solution**:
   - Build > Build Solution (or press F6)
   - The executable will be generated in the `bin/Debug` or `bin/Release` folder

## 📊 Screenshots

*Insert screenshots of your application here*

## 📝 License

This project is licensed under the MIT License - see the LICENSE file for details.

## 🛠 Future Enhancements

- GPU monitoring capabilities
- Storage drive information and monitoring
- Temperature sensors integration
- Export system information to file
- Historical performance graphs

---

## 👨‍💻 About the Developer

This project was created to demonstrate proficiency in:

- C# and .NET Framework development
- Windows Forms UI design
- Multi-threaded application architecture
- System-level programming with WMI
- Performance monitoring techniques

---

*Made with ❤️ by Aurojit Das
