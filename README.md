## TelescopeSimulator-Core

This is an experimental port of the ASCOM .Net Telescope Simulator to .Net 5.0. It depends on the (also experimental) ASCOM.Core libraries which are included as a Sub Module and also can be found here: https://github.com/DanielVanNoord/ASCOM.Core. This is a hybrid driver, it supports both COM and Alpaca REST access. It also has standalone Alpaca builds for Windows and Linux (Mac support coming soon).

This requires .Net Core 5.0 release to build.

This project uses Git submodules. Be sure to clone it recursively and init the project.

There is a prebuilt set of binaries in the releases area on GitHub. 

Note that you can currently only build the Linux Alpaca drivers on Linux operating systems unless you edit the TelescopeSimulator.Alpaca csproj to not check the OS and to only use the cross platform System.Windows.Forms.

It is best to register the server directly using the -register argument. All the server registration, including elevation, should be working. Passing the -register argument from the Visual Studio project debug arguments is currently not working as the server is run within the dotnet.exe tool when started from the debugger rather then standalone. You can attach to it after it is started to debug the registration.

The Linux version uses a .Net Core port of System.Windows.Forms based on the Mono version. This uses System.Drawing.Common and requires at least libx11 (libx11-dev on Debian, libX11-devel on Fedora) and libgdiplus to run. The Mac version has no UI at present. 

By default the Alpaca only builds are bound to local host and port 4321. To bind to all of the host computer's ip addresses (or change the port) you can start it with --urls=http://*:4321.

The ASCOM Hybrid build creates a ASCOM Profile key that can be edited to change the server ip address bindings and ports.

The Alpaca only builds have been tested on Windows, Raspbian, Ubuntu, Fedora and Manjaro. They use a simulated version of the ASCOM profile that does not persist changes at present.

## Known issues:

There are a couple of minor UI glitches in the preview framework.

There is an issue with .Net Core COM objects and ArrayLists (Get SupportedActions) and the current platforms implementation of DriverAccess. Basically ArrayLists are exposed across as just IEnumerable. The current platform fails to convert them because it tries to directly cast to ArrayList. The fix is very straight forward on the platform side and should be released with the next version.

.Net Core COM does not fully support dynamic dispatch yet. This is on the roadmap to be fixed: https://github.com/dotnet/coreclr/issues/24246. For now you need to provide the interface to use the COM system with dynamic on NetFX, NetCore, and Powershell. As ASCOM.DriverAccess already does this most end uses should be reasonably unaffected. 

The Telescope simulator currently uses a virtualized version of ASCOM Profile that does not save settings across resets. Likewise the logger logs to the console. These will be switched to cross platform libraries moving forward.

There are a few improvements to the ASCOMStandard libraries that will be applied to this as they become available.

Currently Linux builds need to be built on Linux operating systems unless the TelescopeSimulator.Alpaca csproj is edited to remove the OS check and only use the cross platform System.Windows.Forms.