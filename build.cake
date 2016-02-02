//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

// Solution
var rootFolder = "./Ets.Mobile";
var solutionPath = rootFolder + "/Ets.Mobile.WindowsPhone.sln";
var allProjectsBinPath = rootFolder + "/**/bin";

// Tests
var testFolder = rootFolder + "/Ets.Mobile.Tests/";
var testProjects = new List<string>()
{
  "Ets.Mobile.Client.Tests/Ets.Mobile.Client.Tests.csproj"
};
var testBinPath = testFolder + "Ets.Mobile.Client.Tests/bin";

// Configurations
var target = Argument("target", "Default");
var releaseConfiguration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// PREPARATION
//////////////////////////////////////////////////////////////////////

// Define directories for tests.
for(var ind = 0; ind < testProjects.Count; ind++)
{
  testProjects[ind] = testFolder + testProjects[ind];
}

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
  .Does(() => {
    CleanDirectories(allProjectsBinPath);
  });

Task("Restore-NuGet-Packages")
  .IsDependentOn("Clean")
  .Does(() => {
    NuGetRestore(solutionPath, new NuGetRestoreSettings()
    {
      Source = new List<string>()
      {
        "https://api.nuget.org/v3/index.json",
        "https://www.nuget.org/api/v2/curated-feeds/microsoftdotnet/",
        "http://nuget.syncfusion.com/MzAwMTE1LDEz",
        "http://nuget.syncfusion.com/MzAwMTE1LDEw"
      }
    });
  });

Task("Build")
  .IsDependentOn("Restore-NuGet-Packages")
  .Does(() => {
     // Since we use AppVeyor (with Windows Env), we'll use MSBuild
     MSBuild(solutionPath, settings =>
       settings
          .SetConfiguration(releaseConfiguration)
          .SetPlatformTarget(PlatformTarget.ARM)
     );

     // Build test projects in x86
     foreach(var project in testProjects)
     {
       MSBuild(project, settings =>
         settings
            .SetConfiguration(releaseConfiguration)
            .SetPlatformTarget(PlatformTarget.x86)
            .SetMSBuildPlatform(MSBuildPlatform.x86)
       );
     }
  });

Task("Run-Unit-Tests")
   .IsDependentOn("Build")
   .Does(() => {
     XUnit2(testBinPath + "/x86/" + releaseConfiguration + "/*.Tests.dll", new XUnit2Settings {
        ToolPath = "./tools/xunit.runner.console/tools/xunit.console.x86.exe"
        });
   });

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Run-Unit-Tests");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);
