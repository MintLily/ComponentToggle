﻿using System.Reflection;
using MelonLoader;
using System;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle(ComponentToggleExtended.BuildInfo.Name)]
[assembly: AssemblyDescription(ComponentToggleExtended.BuildInfo.Description)]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("LilyMod")]
[assembly: AssemblyProduct(ComponentToggleExtended.BuildInfo.Name)]
[assembly: AssemblyCopyright("Copyright ©  2021")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("17200b04-56eb-45c9-8ab3-625ada2349de")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion(ComponentToggleExtended.BuildInfo.Version)]
[assembly: AssemblyFileVersion(ComponentToggleExtended.BuildInfo.Version)]



[assembly: MelonGame("VRChat", "VRChat")]
[assembly: MelonInfo(typeof(ComponentToggleExtended.Main),
    ComponentToggleExtended.BuildInfo.Name,
    ComponentToggleExtended.BuildInfo.Version,
    ComponentToggleExtended.BuildInfo.Author,
    ComponentToggleExtended.BuildInfo.DownloadLink)]
//[assembly: MelonColor(ConsoleColor.Magenta)]
