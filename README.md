## TelescopeSimulator-Core

This is an experimental port of the ASCOM .Net Telescope Simulator to .Net Core 3.0. It depends on the (also experimental) ASCOM.Core libraries which are included as a Sub Module and also can be found here: https://github.com/DanielVanNoord/ASCOM.Core. This is a hybrid driver, it supports both COM and Alpaca REST access. It also has standalone Alpaca builds for Windows, Linux and Mac.

This requires .Net Core 3.0 release to build. You can download this here: https://dotnet.microsoft.com/download/dotnet-core/3.0 or install it through Visual Studio. This can be used with Visual Studio 2019.3 or higher.

There is a prebuilt set of binaries in the releases area on GitHub. 

It is best to register the server directly using the -register argument. All the server registration, including elevation, should be working. Passing the -register argument from the Visual Studio project debug arguments is currently not working as the server is run within the dotnet.exe tool when started from the debugger rather then standalone. You can attach to it after it is started though.

The Linux version uses a .Net Core port of System.Windows.Forms based on the Mono version. This uses System.Drawing.Common and requires at least libx11 and libgdiplus to run. The Mac version has no UI at present. 

By default the Alpaca only builds are bound to local host and port 4321. To bind to all of the computers ip addresses (or change the port) you can start it with --urls=http://*:4321.

The Alpaca only builds have been tested on Windows, Raspbian and Ubuntu. They use a simulated version of the ASCOM profile that does not persist changes at present.

Known issues:
There are a couple of minor UI glitches in the preview framework.

There is an issue with .Net Core COM objects and ArrayLists (Get SupportedActions) and the current platforms implementation of DriverAccess. Basically ArrayLists are exposed across as just IEnumerable. The current platform fails to convert them because it tries to directly cast to ArrayList. The fix is very straight forward on the platform side. I have already fixed the .Net Core driver access library and built and started to test the platform 6 DriveAccess. I will submit a pull request to the main platform after some more testing is complete.