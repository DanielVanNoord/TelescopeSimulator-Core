## TelescopeSimulator-Core

This is an experimental port of the ASCOM .Net Telescope Simulator to .Net Core 3.0. It depends on the (also experimental) ASCOM.Core libraries which are included as a Sub Module and also can be found here: https://github.com/DanielVanNoord/ASCOM.Core. Because this includes WinForms and COM Objects it is only for the Windows platform.

This requires .Net Core 3.0 release 7 or higher to build. You can download this here: https://dotnet.microsoft.com/download/dotnet-core/3.0. Once this is installed you need to enable preview features in Visual Studio 2019.2 (Tools > Options > Environment > Preview Features) or install the current Visual Studio preview.

There is a prebuilt set of binaries in the releases area on GitHub. 

It is best to register the server directly using the -register argument. All the server registration, including elevation, should be working. Passing the -register argument from the VisualStudio project debug arguments is currently not working as the server is run within the dotnet.exe tool when started from the debugger rather then standalone. You can attach to it after it is started though.

Known issues:
There are a couple of minor UI glitches in the preview framework. I have not ported the ASCOM controls yet so I removed them for now.

There is an issue with .Net Core COM objects and ArrayLists (Get SupportedActions) and the current platforms implementation of DriverAccess. Basically ArrayLists are exposed across as just IEnumerable. The current platform fails to convert them because it tries to directly cast to ArrayList. The fix is very straight forward on the platform side. I have already fixed the .Net Core driver access library and built and started to test the platform 6 DriveAccess. I will submit a pull request to the main platform after some more testing is complete.