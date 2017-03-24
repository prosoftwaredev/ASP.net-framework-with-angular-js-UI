# ASP.net-framework-with-angular-js-UI
.NET app build front end with angular Js. customized rollover and timeslip module by realsoft dev

1. System Requirements

1.1 Supported Architectures

x86
x64
ia64 (some features are not supported on ia64, for example, Windows Presentation Foundation (WPF))
1.2 Supported Operating Systems

Windows XP SP3
Windows Server 2003 SP2
Windows Vista SP1
Windows 7
Windows Server 2008 (not supported on Server Core Role)
Windows Server 2008 R2 (not supported on Server Core Role)
1.3 Hardware Requirements

Minimum Available Hard Disk Space:
x86: 850 MB
x64: 2 GB
Processor and RAM:
Minimum: Pentium 1 GHz with 512 MB RAM
1.4 Other System Requirements

Windows Installer 3.1
Internet Explorer 5.01
2. Known Issues

2.1 Installation

2.1.1 Full Framework (Install)

2.1.1.1 Cannot load type 'System.ServiceModel.Activation.HttpModule' after modifying the .NET Framework 3.5 with the .NET Framework 4 installed

This issue may be caused by the following scenarios:

Uninstalling the .NET Framework 3.5 on Windows 2003 Server and Windows XP when the .NET Framework 4 is installed.
Enabling the .NET Framework 3.0 WCF HTTP Activation when the .NET Framework 4 is installed.
Installing or repairing the .NET Framework 3.5 when the .NET Framework 4 is installed.
The current version of the .NET Framework 4 is installed when a prerelease version has already been installed.
The full text of the error is as follows:

Could not load type 'System.ServiceModel.Activation.HttpModule' from assembly 'System.ServiceModel, Version=3.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089'.

Description: An unhandled exception occurred during the execution of the current web request. Please review the stack trace for more information about the error and where it originated in the code.

To resolve this issue:

At a command prompt, navigate to %windows%\Microsoft.Net\Framework\<latest version>\
Execute the following command: aspnet_regiis.exe /iru
2.1.1.2 Uninstalling the .NET Framework 4 Beta 2 leaves unused "isapiCgiRestriction" entries in the applicationHost.config file on Windows Vista, Windows Server 2008, and Windows 7

On a computer that has IIS 7 or IIS 7.5 enabled and has the .NET Framework 4 installed, uninstalling the Beta 2 release leaves unused "isapiCgiRestriction" entries in the applicationHost.config file. This occurs on Windows Vista, Windows Server 2008, and Windows 7. The unused entries do not affect Web server functionality. Later releases of the .NET Framework 4 can be safely installed on the same computer, because subsequent installations will update "isapiCgiRestriction" entries.

To resolve this issue:

Delete the unused "isapiCgiRestriction" entries from the applicationHost.config file. However, this step is not required, because the entries that are left after uninstallation do not affect the functionality of the product or the ability to install later releases.

2.1.1.3 The .NET Framework 1.0 cannot be installed after the .NET Framework 4 is installed

The .NET Framework 1.0 cannot be installed after the .NET Framework 4 is installed. The .NET Framework 1.0 must be installed before the .NET Framework 4 is installed.

To resolve this issue:

Go to control panel and open programs and features.
Uninstall the .NET Framework 4 Extended.
Uninstall the .NET Framework 4 Client Profile.
Install the .NET Framework 1.0.
Install the .NET Framework 4.
2.1.1.4 The .NET Framework 4 Setup failed to install

The .NET Framework 4 Setup failed to install.

To resolve this issue:

Refer to the .NET Framework 4 Setup troubleshooting guide (http://go.microsoft.com/fwlink/?LinkId=186690)

2.1.1.5 The Windows Presentation Foundation (WPF) 4 Font Cache Service is not completely removed after the .NET Framework 4 is uninstalled (Full Framework)

The Windows Presentation Foundation (WPF) 4 Font Cache Service is not completely removed after the .NET Framework 4 is uninstalled (Full Framework).

Note: This issue affects both the Full version and the Client Profile version of the .NET Framework.

To resolve this issue:

Open a command window in Administrator mode.
Type 'sc delete WPFFontCache_v0400'
"[SC] DeleteService SUCCESS" should be displayed.

When you refresh the services console, the Font Cache should not appear. If this refresh does not fix the issue, restart the computer.

2.1.1.6 Failure during the repair of the .NET Framework 4

If you are notified of a failure when you are trying to repair the .NET Framework 4, and if you have more than one .NET Framework 4 Language Pack installed, you can ignore the failure message. The .NET Framework 4 will be repaired correctly.

To resolve this issue:

In the Control Panel, open the installed programs page.
Uninstall the .NET Framework 4 Extended.
Uninstall the .NET Framework 4 Client Profile.
Reinstall the .NET Framework 4 from Microsoft .NET Framework 4 (Web Installer) on the Microsoft Downloads website.
Reinstall the .NET Framework 4 Language Packs from Microsoft .NET Framework 4 Full Language Pack (x86 x64) on the Microsoft Downloads website.
2.1.1.7 Failure during the repair or uninstall of the .NET Framework 4 Language Pack

If you are notified of a failure when you are trying to repair or uninstall the .NET Framework 4 Language Pack, and if you have more than one .NET Framework 4 Language Pack installed:

To resolve this issue:

Scenario 1: If you have both the .NET Framework 4 Client Profile and the .NET Framework 4 Extended installed.

Run the .NET Framework 4 Language Pack Setup for your selected language from Microsoft .NET Framework 4 Full Language Pack (x86 x64) on the Microsoft Downloads website.
Depending on which action you want to take, click either Repair or Uninstall.
Scenario 2: If you have only the .NET Framework 4 Client Profile installed.

Run the .NET Framework 4 Language Pack Setup for your selected language from Microsoft .NET Framework 4 Client Language Pack (x86 x64) on the Microsoft Downloads website.
Depending on which action you want to take, click either Repair or Uninstall.
2.1.2 Client Profile (Install)

2.1.2.1 The .NET Framework 1.0 cannot be installed after the .NET Framework 4 Client Profile is installed

The .NET Framework 1.0 cannot be installed after the .NET Framework 4 Client Profile is installed. The .NET Framework 1.0 must be installed before the .NET Framework 4 Client Profile is installed.

To resolve this issue:

Go to control panel and open programs and features.
Uninstall the .NET Framework 4 Client Profile.
Install the .NET Framework 1.0.
Install the .NET Framework 4 Client Profile.
2.1.2.2 The Windows Presentation Foundation (WPF) 4 Font Cache Service is not completely removed after the .NET Framework 4 is uninstalled (Client Profile)

When the .NET Framework 4 is uninstalled, the WPF Font Cache service might not be completely uninstalled.

Although the WPF Font Cache service is no longer usable after uninstallation, the "Windows Presentation Foundation Font Cache 4.0.0.0" services entry is still displayed in the Services console.

On Windows Vista and Windows Server 2008, the services console "Description" field will say: "<Failed to Read Description. Error Code: 2 >". On Windows XP and Windows Server 2003, the "Description" field will still display the correct string.

Reinstallation of the .NET Framework will repair this. No other effect is known.

Note: This issue affects both the Client Profile version and the Full version of the .NET Framework.

To resolve this issue:

Open a command window in Administrator mode.
Type 'sc delete WPFFontCache_v0400'
"[SC] DeleteService SUCCESS" should be displayed.

When you refresh the services console, the Font Cache should not appear. If this refresh does not fix the issue, restart the computer.

2.1.2.3 The .NET Framework 4 Client Profile Setup failed to install

The .NET Framework 4 Client Profile Setup failed to install.

To resolve this issue:

Refer to the .NET Framework 4 Setup troubleshooting guide (http://go.microsoft.com/fwlink/?LinkId=186690)

2.2 Uninstallation

2.2.1 Full Framework (Uninstall)

2.2.1.1 Uninstalling the .NET Framework 4 Beta 2 leaves unused "isapiCgiRestriction" entries in the applicationHost.config file on Windows Vista, Windows Server 2008, and Windows 7

On a computer that has IIS 7 or IIS 7.5 enabled and has the .NET Framework 4 installed, uninstalling the Beta 2 release leaves unused "isapiCgiRestriction" entries in the applicationHost.config file. This occurs on Windows Vista, Windows Server 2008, and Windows 7. The unused entries do not affect Web server functionality. Later releases of the .NET Framework 4 can be safely installed on the same computer, because subsequent installations will update "isapiCgiRestriction" entries.

To resolve this issue:

Delete the unused "isapiCgiRestriction" entries from the applicationHost.config file. However, this step is not required, because the entries that are left after uninstallation do not affect the functionality of the product or the ability to install later releases.

2.2.1.2 The WPF 4.0 FontCache Service is not completely removed after NET4 uninstall (Full Framework)

The workaround to completely remove this orphaned FontCache service is:

Open a command window in Administrator mode
Enter: 'sc delete WPFFontCache_v0400'
You should see: "[SC] DeleteService SUCCESS".

If you refresh the services console the FontCache should not show now. A reboot may be required if refershing the the services console did not fix the issue.

(Note: this issue if for Full Framework and is a copy of the same Readme issues as 877240 which was for Client Profile )

To resolve this issue:

The workaround to completely remove this orphaned FontCache service is:

Open a command window in Administrator mode
Enter: 'sc delete WPFFontCache_v0400'
You should see: "[SC] DeleteService SUCCESS".

If you refresh the services console the FontCache should not show now. A reboot may be required if refershing the the services console did not fix the issue.

2.2.2 Client Profile (Uninstall)

2.2.2.1 The WPF 4.0 FontCache Service is not completely removed after NET4 uninstall (Client Profile)

When uninstalling .NET 4.0 from Vista/XP/w2k3/W2k8 the WPF font cache service is not cleanly uninstalled.

Although the WPF font cache service is no longer usable after uninstall, the "Windows Presentation Foundation Font Cache 4.0.0.0" services entry is still left behind and is visible in the Services console.

On Vista and W2k8 the services console "Description" field will say: "<Failed to Read Description. Error Code: 2 >". On XP/w2k3 the "Description" field will still display the correct string.

The Framework re-install will repair this. No other effect is known.

Note: this isssue is for both Net4 Client Profile and NET4 Full

To resolve this issue:

The workaround to completely remove this orphaned FontCache service is:

Open a command window in Administrator mode
Enter: 'sc delete WPFFontCache_v0400'
You should see: "[SC] DeleteService SUCCESS".

If you refresh the services console the FontCache should not show now. A reboot may be required if refershing the the services console did not fix the issue.

(Note: this issue if for Client Profile and is a copy of the same Readme issues as 888322 which was for Full.

2.3 Product Issues

2.3.1 General Issues

There are no known issues.

2.3.2 ASP.NET

2.3.2.1 After you install the .NET Framework 4 on Windows 7, you can no longer configure aspnet.config files for individual application pools on IIS 7.5

After you install the .NET Framework 4 on a client or server computer that is running Windows 7 and that has IIS 7.5 enabled, the option to configure ASP.NET configuration files for different application pools stops working. This occurs because installing the .NET Framework 4 causes a slight change to the default behavior for common language runtime (CLR) initialization. When the .NET Framework 4 is installed, IIS 7.5 on Windows 7 calls into a native ASP.NET 4 DLL to perform CLR initialization, and this initialization logic does not enable the use of different configuration files.

To resolve this issue:

Because the CLR initialization logic is fundamentally the same for .NET Framework 4 and for IIS 7.5 (except for the configuration-file side effect), you can reconfigure IIS 7.5 so that it no longer delegates CLR initialization to ASP.NET 4. You can do this in two ways.

Option 1
----------
In the IIS 7.5 applicationHost.config file, set the default value of the "managedRuntimeLoader" attribute to an empty string, as in the following example:

<applicationPools>
<applicationPoolDefaults managedRuntimeLoader="" />
</applicationPools>

Option 2
----------
In the IIS 7.5 IIS_Schema.xml file, set "defaultValue" in the attribute named "managedRuntimeLoader" to an empty string. For example, the attribute might originally resemble the following example:

<attribute name="managedRuntimeLoader" type="string" defaultValue="webengine4.dll" />

Change it to the following markup:

<attribute name="managedRuntimeLoader" type="string" defaultValue="" />

2.3.2.2 Unregistering and reregistering ASP.NET 4 on Windows XP and Windows Server 2003 causes an empty value for the ASP.NET version on the ASP.NET property tab in the IIS MMC

On Windows XP and Windows Server 2003 (all versions), if you unregister ASP.NET 4 from IIS and then reregister it, the IIS MMC displays an empty value for the ASP.NET version list on the ASP.NET tab. The following sequence of steps will cause this issue:

Using the ASP.NET 4 version of aspnet_regiis, run "aspnet_regiis -u"
Using the ASP.NET 4 version of aspnet_regiis, run "aspnet_regiis -i -enable"
To resolve this issue:

In the ASP.NET version list in the IIS MMC, manually select the version of ASP.NET that you want and then click the "Apply" button.

2.3.2.3 ASP.NET compilation tasks on Windows Vista, Windows Server 2008, and Windows 7 might fail because the IIS worker process does not have write permissions to the Windows temporary directory

Some ASP.NET compilation tasks on Windows Vista, Windows Server 2008, and Windows 7 might fail because the IIS worker process does not have write permissions to the Windows temporary directory (%WINDOWS%\Temp). When you try to compile items such as Web service references that depend on WSDL files, you might see errors such as "Parser Error Message: Unable to generate a temporary class."

This error occurs if IIS is enabled on the computer and the .NET Framework 4 is installed, but the ASP.NET and the .NET Extensibility features have not been enabled.

To resolve this issue:

Option 1
----------
Explicitly grant write permissions to the Windows temporary directory (%WINDOWS%\Temp) for the IIS worker process account. One way to do this is to grant write access to a group that includes the worker process account, for example, the IIS_IUSRS group.

Option 2
---------
Enable the ASP.NET and the .NET Extensibility features. In Windows Control Panel, open "Programs", and under "Programs and Features", click "Turn Windows features on and off". In the "Windows Features" dialog box, open the "Internet Information Services" node, then "World Wide Web Services", and then "Application Development Features". Enable the following features:

.NET Extensibility
ASP.NET

2.3.2.4 Trying to load precompiled Web assemblies that are deployed in the GAC fails and throws a "SecurityException" exception when the Web site is running in partial trust

You can precompile ASP.NET Web sites by using the aspnet_compiler.exe command-line tool. If you sign the resulting assemblies with a key, you can deploy assemblies in the GAC instead of in the Bin folder of the Web site.

In ASP.NET 4, if a Web site running in partial trust tries to load the assemblies from the GAC, a "System.Security.SecurityException" exception is thrown. This occurs because, by default, ASP.NET 4 uses a newer code access security (CAS) implementation than earlier versions of ASP.NET. In the new CAS implementation, precompiled and signed assemblies that are deployed in the GAC must be explicitly marked by using the "SecurityTransparent" attribute.

To resolve this issue:

Option 1
--------
Before you compile it, mark the assembly by using the "SecurityTransparent" attribute, as shown in the following example:

[assembly:System.Security.SecurityTransparentAttribute]

Option 2
--------
Add a "compilerOptions" setting to the Web.config file for the site, as described in the article "How to: Create Versioned Assemblies for Precompiled Web Sites" (http://msdn.microsoft.com/en-us/library/ms228042.aspx). As part of this process, add the following line to the AssemblyInfo.vb or AssemblyInfo.cs file that is referenced by the "compilerOptions" setting:

[assembly:System.Security.SecurityTransparentAttribute]

Option 3
--------
Create a dummy class library that contains the following attribute:

[assembly:System.Security.SecurityTransparentAttribute]

Compile the class library into an assembly, and then run the aspnet_merge.exe command-line tool on the precompiled Web site output by using the "copyattrs" option, as shown in the following example:

aspnet_merge c:\MyApplicationRootDirectory -copyattrs assemblyfile.dll

For the DLL name, use the name of the dummy class library that is marked by using the "SecurityTransparent" attribute.

Option 4
--------
Temporarily revert to the older CAS mode by setting the "legacyCasModel" attribute of the "trust" element to "true" in the Web.config file for the site, as shown in the following example:

<trust level="Medium" legacyCasModel="true"/>

After you have made this change, we recommend that you use one of the other options to add the "SecurityTransparent" attribute to precompiled assemblies. You can then remove the "legacyCasModel" attribute and run the Web site in the new CAS mode.

2.3.2.5 ASP.NET and WCF applications might fail to start in IIS 7 Integrated mode

If new configuration sections are added to the application Web.config file of an ASP.NET or Windows Communication Foundation (WCF) application, the application will fail to start when it is running in IIS 7 Integrated mode.

For example, if a <standardEndpoints> configuration section is added to the Web.config file of a WCF application, the application will not start when it is running in IIS 7 Integrated mode. Instead, IIS 7 will return a configuration validation error, because the new configuration section is not recognized by the IIS 7 configuration system.

To resolve this issue:

Download and install a publicly available hotfix for this issue. The hotfix is available at http://support.microsoft.com/kb/958854. Alternatively, you can install Windows Vista SP 2, which includes the fix. Windows 7 and Windows Server 2008 R2 do not have this issue because these operating systems already include the required fix.

2.3.2.6 Re-registering ASP.NET 4 might be required on Windows Vista, Windows Server 2008, Windows 7, and Windows Server 2008 R2

ASP.NET 4 must be re-registered if IIS 7/7.5 or the IIS7/7.5 .NET Extensibility feature is enabled *after* the .NET Framework 4 has already been installed on the computer. ASP.NET 4 must also be re-registered if the .NET Extensibility feature is removed when the .NET Framework 4 is installed on the computer.

For both cases, re-registration is required because the operating system installation and uninstallation process for IIS7 and IIS 7.5 and for the .NET Extensibility feature were not designed for the scenario where a later version of the .NET Framework already exists on the computer.

To resolve this issue:

To re-register ASP.NET 4, run the following command:

aspnet_regiis -iru -enable

Make sure that you use the version of aspnet_regiis.exe that is installed in the .NET Framework 4 installation directory.

2.3.2.7 The ASP.NET management console (MMC) tab might not be displayed when you administer a remote Web server

The ASP.NET tab might not be displayed if you run the management console (MMC) on a local computer when you administer a remote Web server. This occurs when you use the IIS 6 management tool to remotely manage a Web server that has ASP.NET installed, and when the local computer is running Windows Server 2008 x64, Windows 7, or Windows Server 2008 R2 (either x86 or x64).

To resolve this issue:

There is no workaround.

2.3.2.8 Running the ASP.NET 2.0 version of "aspnet_regiis -ua" fails to unregister other versions of ASP.NET, including ASP.NET 4

Running the ASP.NET 2.0 version of the "aspnet_regiis -ua" command on Windows Vista, Windows Server 2008, Windows 7, or Windows Server 2008 R2 causes the following error:

The request is not supported.

This occurs because the ASP.NET 2.0 version of the "aspnet_regiis" command cannot detect that a later version of ASP.NET exists on the computer.

To resolve this issue:

Run the ASP.NET 4 version of the "aspnet_regiis -ua" command to unregister all versions of ASP.NET on the computer.

2.3.2.9 Running "aspnet_regiis -i" on Windows Server 2003 does not recursively force virtual directories to be upgraded to ASP.NET 4

For ASP.NET 2.0, the "aspnet_regiis -i" command recursively upgrades all virtual directories on Windows Server 2003 to use ASP.NET 2.0. For ASP.NET 4, the "aspnet_regiis -i" command on Windows Server 2003 upgrades only the root of IIS 6 to ASP.NET 4. If any virtual directories below the root are explicitly set to run a specific version of ASP.NET, those virtual directories retain the version of ASP.NET that was explicitly set instead of inheriting the ASP.NET 4 setting from the root directories.

To resolve this issue:

Run the ASP.NET 4 versions of either of the following commands:

aspnet_regiis -s

aspnet_regiis -r

These commands force a recursive update of all virtual directories to ASP.NET 4.

2.3.2.10 Unregistering ASP.NET 2.0 breaks ASP.NET 4 performance counters

Unregistering ASP.NET 2.0 on any operating system version where ASP.NET 4 is already registered corrupts some performance counter registrations for ASP.NET 4. This occurs because the ASP.NET 2.0 unregistration process cannot detect that a later version of ASP.NET is installed on the computer. As a result, when you use certain ASP.NET 4 performance counters, errors such as the following ones might appear in the application event log:

"Unable to locate the open procedure "%pef_counter_name%" in DLL "%WINDOWS%\Microsoft.NET\Framework\v4.0.NNNNN\aspnet_perf.dll" for the "ASP.NET" service."

"Performance counter data collection from the "ASP.NET" service has been disabled due to one or more errors generated by the performance counter library for that service."

To resolve this issue:

Run the ASP.NET 4 version of the "aspnet_regiis -iru" command. This re-registers the ASP.NET 4 performance counters.

2.3.2.11 SQL Server Express user instances do not work with Web application projects under IIS 6 or IIS 7 or with applications under IIS 7.5

By default, ASP.NET 4 Web projects and Web applications that rely on SQL Server Express user instances do not work under the following scenarios:

A Web application project (WAP) is hosted as a virtual directory on any version of IIS. This is because SQL Server Express user instances require specific file permissions for the user's Documents folder and the default IIS service account (NETWORK SERVICE) does not have these permissions.
A Web site is hosted on IIS 7.5 running on Windows 7 or on Windows Server 2008 R2. This is because the default security credentials for IIS 7.5 application pools are not based on NETWORK SERVICE.
To resolve this issue:

For details about how to address these issues, see the article at

http://go.microsoft.com/fwlink/?LinkID=160102

2.3.2.12 Configuration errors are thrown by ASP.NET 4 or IIS 7 when related sections exist in application-level Web.config files

In ASP.NET 4, the default Web.config file has been substantially reduced in size. As a result, IIS 7 (on Windows Vista and on Windows Server 2008) and IIS 7.5 (on Windows Server 2008 R2) will throw configuration errors. The exact errors depend on the updates that have been installed on the operating system, and on the kind of configuration information that is contained in application-level Web.config files.

Windows Vista SP1 or Windows Server 2008 SP1, where neither hotfix KB958854 nor SP2 are installed. In this configuration, the IIS 7 configuration system incorrectly merges the managed configuration of an application by comparing the application-level Web.config file to the ASP.NET 2.0 machine.config files. Because of this, application-level Web.config files from the .NET Framework 3.5 or the .NET Framework 4 must have a <system.web.extensions> configuration section in order not to cause an IIS 7 validation failure. Manually modified application-level Web.config file entries that do not precisely match the original boilerplate configuration section definitions that were introduced with Visual Studio 2008 will cause configuration errors. (The default configuration entries that are generated by Visual Studio 2008 do work). A common problem is that manually modified Web.config files leave out the configuration attributes for "allowDefinition" and "requirePermission" that are found on various configuration section definitions. As a result, there is a mismatch between the abbreviated configuration section in application-level Web.config files and the complete definintion in the ASP.NET 4 machine.config file. Therefore, at run time, the ASP.NET 4 configuration system throws a configuration error.

Windows Vista SP2, Windows Server 2008 SP2, Windows 7, Windows Server 2008 R2, and also Windows Vista SP1 and Windows Server 2008 SP1 where hotfix KB958854 is installed. In this scenario, the IIS 7 and IIS 7.5 native configuration system returns a configuration error because it performs a text comparison on the "type" attribute that is defined for a managed configuration section handler. Because all Web.config files that are generated by Visual Studio 2008 and Visual Studio 2008 SP1 have "3.5" in the type string for the <system.web.extensions> (and related) configuration sections, and because the ASP.NET 4 machine.config file has "4.0" in the "type" attribute for the same configuration sections, applications that are generated in Visual Studio 2008 or Visual Studio 2008 SP1 always fail configuration validation in IIS 7 and IIS 7.5.

To resolve this issue:

For the first scenario, update the application-level Web.config file by including the boilerplate configuration text from a Web.config file that was generated automatically by Visual Studio 2008.

For the second scenario, delete or comment out all the <system.web.extensions> configuration section definitions and configuration section group definitions from the application level Web.config file.

2.3.2.13 No parameter data is ever passed to the System.Web.Hosting.IProcessHostPreloadClient.Preload method

The System.Web.Hosting.IProcessHostPreloadClient.Preload method takes a string array as an input parameter. However, there is no way to set this data, and no information is ever passed in this parameter.

To resolve this issue:

Earlier preview versions of the IIS 7.5 autostart feature supported a way to configure one or more string values to pass to the ASP.NET 4 IProcessHostPerloadClient.Preload method. However, that functionality was removed before the final release of Windows 7 and of Windows Server 2008 R2.

2.3.2.14 The IIS7/IIS7.5 .NET Extensibility feature on Windows Vista, Windows Server 2008, Windows 7, and Windows Server 2008 R2 is not integrated with ASP.NET 4

The IIS 7 and IIS 7.5 .NET Extensibility feature is a configuration option that is available in the "Windows Features" dialog box to install or uninstall IIS 7 or IIS 7.5 features. The feature is located in the following node:

Internet Information Services > World Wide Web Services > Application Development Features > .NET Extensibility

On Windows Vista, Windows Server 2008, Windows 7, and Windows Server 2008 R2, the .NET Extensibility feature affects only ASP.NET 2.0 integration with IIS 7 or IIS 7.5. It has no effect on registering or unregistering ASP.NET 4 with IIS 7 or IIS 7.5.

To resolve this issue:

To manage ASP.NET 4 integration with IIS 7 or IIS 7.5, use the ASP.NET 4 version of the "aspnet_regiis.exe" command.

2.3.2.15 ASP.NET 2.0 applications that run on IIS 6 might generate errors such as "System.Web.HttpException: Path '/[yourApplicationRoot]/eurl.axd/[Value]' was not found."

ASP.NET 2.0 applications that run on IIS 6 (in either Windows Server 2003 or Windows Server 2003 R2) might generate errors such as the following one:

System.Web.HttpException: Path '/[yourApplicationRoot]/eurl.axd/[Value]' was not found.

This error occurs only after ASP.NET 4 has been enabled on IIS 6. This error occurs because when ASP.NET detects that a Web site is configured to use ASP.NET 4, a native component of ASP.NET 4 passes extensionless URLs to the managed portion of ASP.NET for further processing.

However, if virtual directories that are below an ASP.NET 4 Web site are configured to use ASP.NET 2.0, processing the extensionless URL in this way produces a modified URL that contains "eurl.axd", which is then sent to the ASP.NET 2.0 application. ASP.NET 2.0 cannot recognize the "eurl.axd" format. Therefore, ASP.NET 2.0 tries to find a file named "eurl.axd" and execute it. Because no such file exists, the request fails with an "HttpException" exception.

To resolve this issue:

Option 1
--------
If ASP.NET 4 is not required in order to run the Web site, remap the site to use ASP.NET 2.0 instead.

Option 2
---------
If ASP.NET 4 is required in order to run the Web site, move any child ASP.NET 2.0 virtual directories to a different Web site that is mapped to ASP.NET 2.0.

Option 3
---------
If it is not practical to remap the Web site to ASP.NET 2.0 or to change the location of a virtual directory, explicitly disable extensionless URL processing in ASP.NET 4. Use the following procedure:

1. 1. In the Windows registry, open the following node:

HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\ASP.NET\4.0.<build#>

Note: <build#> is the build number of the release version of the .NET Framework 4.

2. Create a DWORD value named "EnableExtensionlessUrls".

3. Set "EnableExtensionlessUrls" to 0. This disables extensionless URL behavior.

4. Save the registry value and close the registry editor.

5. Run the "iisreset" command-line tool, which causes IIS to read the new registry value.

Note: Setting "EnableExtensionlessUrls" to 1 enables extensionless URL behavior. This is the default setting if no value is specified.

2.3.2.16 Web sites that use Entity Framework and were created by using prerelease versions of ASP.NET 4 stop working because of missing assembly references

References to namespaces and assemblies that are required by Web projects that use Entity Framework have been removed from the RTM version of the root Web.config file. As a result, Dynamic Data Web sites that use EntityDataSource, as well as Web applications that use Entity Framework that were created by using prerelease versions of ASP.NET 4 will fail and report compilation errors.

To resolve this issue:

You can insert the missing assembly and namespace references into the Web.config file of the application. The following example shows the assembly and namespace elements that must be manually inserted in the application-level Web.config file.

<system.web>

<compilation>
<assemblies>
<add assembly="System.Data.Entity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" />
<add assembly="System.Web.Entity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"/>
<add assembly="System.Data.Linq, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" />
<add assembly="System.Data.Entity.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" /> 
</assemblies>
</compilation>

<pages>
<namespaces>
<add namespace="System.Data.Entity.Design" />
<add namespace="System.Data.Linq" />
</namespaces>
</pages>

</system.web>

2.3.2.17 Prerelease versions of ASP.NET 4 that run on IIS 7 or IIS 7.5 in Integrated mode might report an unhandled NullReferenceException error thrown from the RoleManagerModule class

Under certain installation orders for the .NET Framework version 2.0 and version 4 on Windows Vista, Windows Server 2008, Windows 7, and Windows Server 2008 R2, ASP.NET 4 applications throw an unhandled NullReferenceException error from the RoleManagerModule class. This occurs when ASP.NET 4 is the only version of ASP.NET that is being registered with IIS 7 or IIS 7.5, and ASP.NET 2.0 has either never been registered with IIS, or ASP.NET 2.0 was unregistered from IIS 7 or IIS 7.5.

In either scenario, the standalone registration of ASP.NET 4 causes incorrect order in the configuration file for two HTTP modules that are used in Integrated-mode applications.

To resolve this issue:

Although this issue is fixed in the ASP.NET 4 release version, prerelease versions of ASP.NET 4 might have specified the incorrect order for the modules. If the unhandled exception is still occurring on a computer that has been upgraded from a prerelease version of ASP.NET 4 to the RTM version, perform the following steps:

1. Open the applicationHost.config file, which is in the following folder:

%windir%\System32\inetsrv\config

2. Find the following element

<location path="" overrideMode="Allow">

In this element is the list of HTTP modules for Integrated mode. The information is in the <modules> element.

3. Locate the element that begins with the following string:

<add name="RoleManager" ...

4. Move the element below the element that begins with the following string:

<add name="DefaultAuthentication"...

5. Save the file.

When you are finished, one portion of the <modules> definition should resemble the following example:

<add name="DefaultAuthentication" type="System.Web.Security.DefaultAuthenticationModule" preCondition="managedHandler" />
<add name="RoleManager" type="System.Web.Security.RoleManagerModule" preCondition="managedHandler" />

2.3.2.18 MVC 2 and ASP.NET 4 Web Forms applications that use URL routing might return HTTP 404 errors when they attempt to process extensionless URLs on IIS 7 and IIS 7.5

MVC 2 and ASP.NET 4 Web Forms applications that use extensionless URLs might return HTTP 404 errors when they run on Windows Vista, Windows Server 2008, Windows 7, or Windows Server 2008 R2. This situation can occur when only the .NET Framework Extensibility option is on while IIS is installed through the Windows Features dialog box. A minimal installation of IIS will not include certain HTTP modules. Because of how ASP.NET and IIS manage HTTP pipeline-event transitions, the missing HTTP modules prevent the ASP.NET URL routing module from running at the appropriate time. As a result, requests for extensionless URLs are not processed by the URL routing module, and a 404 error occurs.

To resolve this issue:

In the "Turn Windows Features On or Off" dialog box of the Windows Control Panel 
"Programs and Features" application, perform the following steps:

1. Navigate to the following node:

Internet Information Services --> World Wide Web Services --> Common HTTP Features

2. Make sure that the "HTTP Error Redirection" option is selected.

-or-

1. Navigate to the following node:

Internet Information Services --> World Wide Web Services --> Performance Features

2. Make sure that the "Static Content Compression" option is selected.

After either option has been selected, click "OK" to save changes.

Re-enabling either the HTTP Error Redirection module or the Static Content Compression module ensures that ASP.NET and IIS correctly synchronize HTTP pipeline events. This enables the URL routing module to process extensionsless URLs.

2.3.2.19 The System.Web.Mobile.dll has been removed from the root Web.config file

In earlier versions of ASP.NET, a reference to the System.Web.Mobile.dll assembly was included in the root Web.config file in the <assemblies> section under <system.web><compilation>. To improve performance, the reference to this assembly was removed.

To resolve this issue:

The System.Web.Mobile.dll assembly is included in ASP.NET 4, but it is deprecated. If you want to use types from the System.Web.Mobile.dll assembly, add a reference to this assembly, either in the root Web.config file or in an application Web.config file. For example, if you want to use any of the (deprecated) ASP.NET mobile controls, you must add to the Web.config file a reference to the System.Web.Mobile.dll assembly.

2.3.2.20 Changes have been made to browser definition files and browser capabilities

The browser definition files have been updated to contain information about new and updated browsers and devices. Older browsers and devices such as Netscape Navigator have been removed, and newer browsers and devices such as Google Chrome and Apple iPhone have been added.

To resolve this issue:

You can use the old browser definition files with ASP.NET 4. The old browser definition files, and documentation for installing them, are contained in the ASP.NET Browser Definition Files release at http://go.microsoft.com/fwlink/?LinkID=186493.

2.3.2.21 ScriptManager.EnableCdn and Localized Microsoft Ajax Files

The localized versions of the Microsoft Ajax JavaScript files, such as MicrosoftAjax.debug.ja.js, will not be added to the Microsoft Ajax Content Delivery Network (CDN) until the localized versions of the .NET Framework 4 are released. Therefore, do not enable the ScriptManager.EnableCdn property when you are using a localized version of the .NET Framework and the CDN.

To resolve this issue:

Wait until the localized versions of the .NET Framework 4 are released before you use the Microsoft Ajax Content Delivery Network (CDN). Until then, make sure that ScriptManager controls in your application do not have EnableCdn="true".

2.3.2.22 Generic ASP.NET Performance Counters Only Report Data from ASP.NET 4 Applications

After ASP.NET 4 is installed, the generic ASP.NET performance counters will only report data from ASP.NET 4 applications. If the generic performance counters are used against ASP.NET 1.1, ASP.NET 2.0, and ASP.NET 3.5 applications, the performance counters will not report any data. Performance data for applications that run earlier versions of ASP.NET must use the versioned ASP.NET performance categories.

The generic ASP.NET performance counters include the following performance counter categories: "ASP.NET" and "ASP.NET Applications".

The versioned ASP.NET performance categories have names that resemble these: "ASP.NET v2.0.50727" and "ASP.NET Apps v2.0.50727".

To resolve this issue:

This behavior is by design. The latest version of ASP.NET installed on a computer "owns" the generic performance counter categories. Therefore, we recommend that you use the versioned performance counter categories when you collect performance data from multiple ASP.NET applications that are running different versions of ASP.NET.

2.3.3 Winforms

There are no known issues.

2.3.4 Parallel Programming

There are no known issues.

2.3.5 Managed Extensibility Framework

There are no known issues.

2.3.6 Entity Framework

There are no known issues.

2.3.7 LINQ to SQL

There are no known issues.

2.3.8 Windows Communication Foundation (WCF)

2.3.8.1 "The system cannot find the file specified" error occurs when a service is started or IIS is reset after the client profile is upgraded

After the .NET Framework 4 is upgraded to the RTM version from Beta 2, the following error may occur when you start services or restart IIS:

"The system cannot find the file specified"

To resolve this issue:

Repair the .NET Framework Client Profile in the Programs application in the control panel.

2.3.9 Windows Presentation Foundation (WPF)

2.3.9.1 Windows Presentation Foundation (WPF) is not supported on ia64

WPF assemblies are not installed or supported on ia64 computers.

To resolve this issue:

There is no workaround. WPF cannot be used on ia64.

2.3.10 Windows Workflow Foundation (WF)

2.3.10.1 Workflow validation does not support the sizeof operator

When a workflow that includes the sizeof operator is validated, an exception is thrown.

To resolve this issue:

Do not use the sizeof operator in workflows.

2.3.11 Client Profile (Product)

2.3.11.1 The .NET Framework 4 Client Profile is not supported on ia64

The .NET Framework 4 Client Profile is not supported on ia64.

To resolve this issue:

If you uninstall the .NET Framework 4 on ia64, be sure to uninstall both the Full version and the Client Profile version.

3. Related Links

Microsoft thanks the following people for working with us to help protect customers:
* Jeroen Frijters

To view a list of potential breaking changes for an ASP.NET developer who is upgrading to the .NET Framework 4, see: http://go.microsoft.com/fwlink/?LinkID=186526. This list is updated as new issues are found.
 

© 2010 Microsoft Corporation. All rights reserved.

Terms of Use | Trademarks | Privacy Statement
