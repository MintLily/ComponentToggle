using System.Resources;
using System.Reflection;
using System.Runtime.InteropServices;
using MelonLoader;

[assembly: AssemblyTitle(ComponentToggle.BuildInfo.Name)]
[assembly: AssemblyDescription(ComponentToggle.BuildInfo.Description)]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany(ComponentToggle.BuildInfo.Company)]
[assembly: AssemblyProduct(ComponentToggle.BuildInfo.Name)]
[assembly: AssemblyCopyright("Created by " + ComponentToggle.BuildInfo.Author)]
[assembly: AssemblyTrademark(ComponentToggle.BuildInfo.Company)]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
//[assembly: Guid("")]
[assembly: AssemblyVersion(ComponentToggle.BuildInfo.Version)]
[assembly: AssemblyFileVersion(ComponentToggle.BuildInfo.Version)]
[assembly: NeutralResourcesLanguage("en")]
[assembly: MelonInfo(typeof(ComponentToggle.Main),
    ComponentToggle.BuildInfo.Name,
    ComponentToggle.BuildInfo.Version,
    ComponentToggle.BuildInfo.Author,
    ComponentToggle.BuildInfo.DownloadLink)]

//[assembly: MelonOptionalDependencies("", "", "", "")]
// Create and Setup a MelonModGame to mark a Mod as Universal or Compatible with specific Games.
// If no MelonModGameAttribute is found or any of the Values for any MelonModGame on the Mod is null or empty it will be assumed the Mod is Universal.
// Values for MelonModGame can be found in the Game's app.info file or printed at the top of every log directly beneath the Unity version.
[assembly: MelonGame("VRChat", "VRChat")]