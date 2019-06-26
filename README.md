# Kofax-Custom-Module-Script-Guide

## <a name=Content></a> Table of Contents
1. [Development Environment](#DevEnv)
2. [Project Settings](#Settings)  
  2.1. [Project Type](#ProjectType)  
  2.2. [Framework](#Framework)  
  2.3. [COM (Component Object Model)](#COM)  
  2.4. [Target Platform](#Target)  
  2.5. [Build](#Build)  
  2.6. [Debugging](#Debugging)  
    &emsp;&ensp;&nbsp;2.6.1. [Debug Information](#DebugInfo)  
    &emsp;&ensp;&nbsp;2.6.2. [Local Tests](#Tests)  
  2.7. [Dependencies](#Dependencies)  
  2.8. [Registration](#Registration)  
  2.9. [Setup](#Setup)  
    &emsp;&ensp;&nbsp;2.9.1. [Setup Form](#SetupForm)  
  2.10. [Release](#Release)  
  2.11. [Register the project on the machine](#ProjectRegistration)  
  2.12. [Install the Script](#Installation)
3. [Rollout](#Rollout)  

## <a name=DevEnv></a> 1. Development Environment

The guide relies on **Visual Studio**

## <a name=Settings></a> 2. Project Settings

### <a name=ProjectType></a> 2.1 Project Type

Windows Service Application

### <a name=Framework></a> 2.2 Framework

.NET 4.6.1 Framework

```
=> Application => Target framwork
```

### <a name=COM></a> 2.3 COM (Component Object Model)

In order for the interfaces to communicate with Kofax, COM visibility must be enabled

```
=> Application => Assembly Information => Make assembly COM-Visible
```

### <a name=Target></a> 2.4 Target Platform

As a 32-bit application, the target platform is x86

```
=> Build => Platform target
```

### <a name=Build></a> 2.5 Build

The files required for Kofax must be stored in the Bin directory of Kofax. This can be found at

```
.....\Program Files (x86)\Kofax\CaptureSS\ServLib\Bin\
```

In order to optimize the development, the output path can be set directly via Visual Studio

```
=> Build => Output path
```

**Do this for Debug and Release**

### <a name=Debugging></a> 2.6 Debugging

#### <a name=DebugInfo></a> 2.6.1 Debug Information

To get complete debug information, the option must be specified

```
=> Build => Advanced => Debugging information
```

#### <a name=Tests></a> 2.6.2 Local Tests

Due to the fact that this application acts as an independent process there is no need for external programs.

### <a name=Dependencies></a> 2.7 Dependencies

The **Kofax.Capture.AdminModule.dll**, **Kofax.Capture.SDK.CustomModule.dll**, **Kofax.Capture.SDK.Data.dll** and **Kofax.DBLite.dll** must be included in the references

```
=> References => Add Reference => Browse => C:\Program Files (x86)\Kofax\CaptureSS\ServLib\Bin\Kofax.ReleaseLib.Interop.dll
```
**Please make sure that each Kofax related dependency is set to "copy local" = false**

### <a name=Registration></a> 2.8 Registration

Take the [.bat file](https://github.com/matthiashermsen/Kofax-Custom-Module-Script-Guide/blob/master/MyCustomModuleInstall.bat) to install the custom module as a Windows service. Run it from the Kofax Bin directory as administrator.

A [.aex file](https://github.com/matthiashermsen/Kofax-Custom-Module-Script-Guide/blob/master/MyCustomModule.aex) is required to install the custom module. This is then stored in the Kofax Bin directory.

Take the [.bat file](https://github.com/matthiashermsen/Kofax-Custom-Module-Script-Guide/blob/master/MyCustomModuleRegister.bat) to register the custom module. Run it from the Kofax Bin directory as administrator.

### <a name=SetupScript></a> 2.9 Setup

#### <a name=SetupForm></a> 2.9.1 Setup Form

The setup form starts a [form](https://github.com/matthiashermsen/Kofax-Custom-Module-Script-Guide/blob/master/MyCustomModule/MyCustomModule/Setup/FrmSetup.cs) initialized by a [UserControl](https://github.com/matthiashermsen/Kofax-Custom-Module-Script-Guide/blob/master/MyCustomModule/MyCustomModule/Setup/UserControlSetup.cs)

### <a name=ReleaseScript></a> 2.10. Release

The [release form](https://github.com/matthiashermsen/Kofax-Custom-Module-Script-Guide/blob/master/MyCustomModule/MyCustomModule/Runtime/FrmMain.cs) is executed during the release.

A [login](https://github.com/matthiashermsen/Kofax-Custom-Module-Script-Guide/blob/master/MyCustomModule/MyCustomModule/Runtime/SessionManager.cs) to Kofax is required.

The [timer](https://github.com/matthiashermsen/Kofax-Custom-Module-Script-Guide/blob/master/MyCustomModule/MyCustomModule/Runtime/PollTimer.cs) launches a [polling](https://github.com/matthiashermsen/Kofax-Custom-Module-Script-Guide/blob/master/MyCustomModule/MyCustomModule/Runtime/BatchManager.cs) for new batches to [process](https://github.com/matthiashermsen/Kofax-Custom-Module-Script-Guide/blob/master/MyCustomModule/MyCustomModule/Runtime/BatchProcessor.cs)

### <a name=ProjectRegistration></a> 2.11. Register the project on the machine

To register the project locally, RegAscEx must be run once as administrator in the console

```
"C:\Program Files (x86)\Kofax\CaptureSS\ServLib\Bin\RegAscEx.exe" MyCustomModule.aex
```

or have a look here [Registration](#Registration).

To run it as a Windows service open [RuntimeService](https://github.com/matthiashermsen/Kofax-Custom-Module-Script-Guide/blob/master/MyCustomModule/MyCustomModule/Runtime/RuntimeService.cs) file, right click and add an Installer to it.

For the service process installer set the **Account** to *Local Service*.

For the service installer setup the **Description**, **Display Name**, **Service Name** and set the **Start Type** to *Automatic*

Update your main file [Program](https://github.com/matthiashermsen/Kofax-Custom-Module-Script-Guide/blob/master/MyCustomModule/MyCustomModule/Runtime/Program.cs)

### <a name=Installation></a> 2.12. Install the Script

The script can be installed via the administration module

```
Tools Tab => Custom Modules => Add => .aex File from the Kofax Bin Directory
```

## <a name=Rollout></a> 3. Rollout

To deliver the script to the customer, the project .exe and .aex file must be placed in the customer's Kofax Bin directory
