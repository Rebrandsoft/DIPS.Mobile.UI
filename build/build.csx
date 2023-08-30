#load "BuildSystem/Steps.csx"
#load "BuildSystem/Build/Xamarin/Android.csx"
#load "BuildSystem/Build/Xamarin/iOS.csx"
#load "BuildSystem/Build/MSBuild.csx"
#load "BuildSystem/Repository.csx"
#load "BuildSystem/Command.csx"
#load "BuildSystem/Nuget.csx"
#load "BuildSystem/dotnet/dotnet.csx"
#load "BuildSystem/AzureDevops.csx"
#load "BuildSystem/Versioning/VersionUtil.csx"
#load "BuildSystem/Git.csx"
#load "BuildSystem/Distribute/AppCenter.csx"
#load "BuildSystem/AppConfig/AppConfig.csx"
#load "BuildSystem/AppConfig/AppConfigManager.csx"
#load "BuildSystem/Helpers/DirectoryHelper.csx"
#load "BuildSystem/Helpers/WriteToFileHelper.csx"

private static string LibraryPackageVersion = "1.1.0";
private static string RootDir = Repository.RootDir();
private static string OutputDir = Path.Combine(RootDir, "output");
private static string ChangeLogPath = Path.Combine(RootDir, "CHANGELOG.MD");
private static string SrcDir = Path.Combine(RootDir, "src");
//Full solution with library and samples app
private static string SolutionPath = SrcDir;
//Libary
private static string LibraryDir = Path.Combine(SolutionPath, "library", "DIPS.Mobile.UI");
private static string LibraryProjectPath = Path.Combine(LibraryDir, "DIPS.Mobile.UI.csproj");
//App
private static string AppDir = Path.Combine(SolutionPath, "app", "Components");
private static string AppProjectPath = Path.Combine(AppDir, "Components.csproj");
private static string AppConfigPath = Path.Combine(AppDir, "Resources", "Raw", "appconfig.json");
//Tests
private static string TestDir = Path.Combine(SrcDir, "tests");
private static string NugetTestSolutionPath = Path.Combine(TestDir,"nugettest");
private static string TestProjectPath = Path.Combine(TestDir, "unittests", "DIPS.Mobile.UI.UnitTests.csproj");

//Distribution

 private static string AppCenter_iOSApiKey = AzureDevops.GetEnvironmentVariable("appcenter.apikey.dips.mobile.ui.ios");
 private static string AppCenter_droidApiKey = AzureDevops.GetEnvironmentVariable("appcenter.apikey.dips.mobile.ui.droid");
private static string AppCenter_iOSAppGroupName = "Components-iOS";
private static string AppCenter_droidAppGroupName = "Components-Droid";
private static string AppCenter_DistributionGroupName = "releases";
private static string ChangelogHeaderPrefix = "## [";
private static string VersionPattern = "[0-11]+.[0-11]+.[0-11]+";

AsyncStep build = () => dotnet.Build(LibraryProjectPath);

AsyncStep test = () => dotnet.Test(TestProjectPath, OutputDir);

AsyncStep pack = async () =>
{
    if (Directory.Exists(OutputDir))
    {
        Directory.CreateDirectory(OutputDir);
    }

    await PackLibrary();
};

AsyncStep packiOS = async () =>
{
    if (!Directory.Exists(OutputDir))
    {
        Directory.CreateDirectory(OutputDir);
    }
    var latestRelease = await AppCenter.GetLatestVersionFromDistributionGroup(AppCenter_iOSAppGroupName, AppCenter_DistributionGroupName, AppCenter_iOSApiKey);
    var nextReleaseId = latestRelease.Id + 1;
    AppConfigManager.InsertAppConfig(AppConfigPath, AppCenter_iOSAppGroupName, AppCenter_DistributionGroupName, AppCenter_iOSApiKey);
    await dotnet.PackiOS(AppProjectPath, OutputDir, VersionUtil.GetLatestVersionFromChangelog(ChangeLogPath));
};

AsyncStep packDroid = async () =>
{
    if (!Directory.Exists(OutputDir))
    {
        Directory.CreateDirectory(OutputDir);
    }

    var latestRelease = await AppCenter.GetLatestVersionFromDistributionGroup(AppCenter_droidAppGroupName, AppCenter_DistributionGroupName, AppCenter_droidApiKey);
    var nextReleaseId = latestRelease.Id + 1;
    AppConfigManager.InsertAppConfig(AppConfigPath, AppCenter_droidAppGroupName, AppCenter_DistributionGroupName, AppCenter_droidApiKey);
    await dotnet.PackAndroid(AppProjectPath, OutputDir, VersionUtil.GetLatestVersionFromChangelog(ChangeLogPath), nextReleaseId.ToString());
};

AsyncStep publishApp = async () =>
{
    if (!Directory.Exists(OutputDir))
    {
        Directory.CreateDirectory(OutputDir);
    }

    if(!(await Git.CurrentBranchIsMain()))
    {
        Logger.LogDebug("Current branch is not main branch, this app will not get published to AppCenter");
        return;
    }


    var releaseNotes = VersionUtil.GetReleaseNoteFromLatestVersion(ChangeLogPath);
    var releaseNotesReadAble = string.Join("\n", releaseNotes);
    var ipaFile = FileHelper.FindSingleFileByExtension(OutputDir, ".ipa");
    var iOSReleaseId = await AppCenter.CreateRelease(AppCenter_iOSAppGroupName, ipaFile.FullName, PlatformTarget.iOS, AppCenter_iOSApiKey);
    await AppCenter.DistributeRelease(iOSReleaseId, AppCenter_iOSAppGroupName, new string[] { AppCenter_DistributionGroupName}, AppCenter_iOSApiKey, releaseNotesReadAble);

    var apkFile = FileHelper.FindSingleFileByExtension(OutputDir, ".apk");
    var droidReleaseId = await AppCenter.CreateRelease(AppCenter_droidAppGroupName, apkFile.FullName, PlatformTarget.Android, AppCenter_droidApiKey);
    await AppCenter.DistributeRelease(droidReleaseId, AppCenter_droidAppGroupName, new string[] { AppCenter_DistributionGroupName}, AppCenter_droidApiKey, releaseNotesReadAble);
};

AsyncStep publish = async () =>
{
    var nupkgFile = FileHelper.FindSingleFileByExtension(OutputDir, ".nupkg");

    //Code sign
    var codeSignPath = AzureDevops.GetEnvironmentVariable("codesign.securefilepath");
    var codesignPw = AzureDevops.GetEnvironmentVariable("nuget.dipsas.certpw");

    await Command.ExecuteAsync("nuget", $"sign {nupkgFile.FullName} -CertificatePath {codeSignPath} -CertificatePassword {codesignPw}  -Timestamper http://timestamp.digicert.com/");

    //Push
    var apiKey = AzureDevops.GetEnvironmentVariable("dipsmobileuiNugetApiKey");
    if (string.IsNullOrEmpty(apiKey))
    {
        throw new Exception("dipsmobileuiNugetApiKey: is not set for this build. Unable to push nuget package");
    }

    await dotnet.NugetPush(nupkgFile.FullName, apiKey, "https://api.nuget.org/v3/index.json");
};

AsyncStep nugetTest = async () =>
{
    //Clear from nuget cache
    var homePath = Environment.GetEnvironmentVariable("HOME");
    var oldNugetPackageFilePath = Path.Combine(homePath, ".nuget", "packages", "dips.mobile.ui");
    if (File.Exists(oldNugetPackageFilePath))
    {
        Directory.Delete(oldNugetPackageFilePath, true);
    }

    //Clear from local packages folder
    var packagesDir = Path.Combine(NugetTestSolutionPath, "packages");
    if (Directory.Exists(packagesDir))
    {
        var files = Directory.EnumerateFiles(Path.Combine(NugetTestSolutionPath, "packages"));
        foreach (var file in files)
        {
            File.SetAttributes(file, FileAttributes.Normal);
            File.Delete(file);
        }
    }
    else
    {
        Directory.CreateDirectory(packagesDir);
    }


    await dotnet.Restore(LibraryDir);
    await dotnet.Build(LibraryProjectPath);
    await PackLibrary(packagesDir);
};

AsyncStep createResourcesPR = async () =>
{
    var prBranchName = "designToken-resources-update";
    
    //checkout new branch
    Logger.LogDebug($"Trying to create {prBranchName}");
    //TODO: await Command.CaptureAsync("git", $"branch -D {prBranchName}"); //Clean it up if its there
    //TODO: await Command.CaptureAsync("git", $"checkout -b {prBranchName}");

    //Where is everything located
    //Generated resources
    var generatedAndroidDir = new DirectoryInfo(Path.Combine(OutputDir, "android"));
    var generatedDotnetMauiDir = new DirectoryInfo(Path.Combine(OutputDir, "dotnet", "maui"));

    var generatedAndroidColorFile = generatedAndroidDir.GetFiles().FirstOrDefault(f => f.Name.Equals("colors.xml"));
    var generatedDotnetMauiColorsDir = generatedDotnetMauiDir.GetDirectories().FirstOrDefault(d => d.Name.Equals("Colors"));
    var generatedDotnetMauiIconsDir = generatedDotnetMauiDir.GetDirectories().FirstOrDefault(d => d.Name.Equals("Icons"));
    var generatedDotnetMauiSizesDir = generatedDotnetMauiDir.GetDirectories().FirstOrDefault(d => d.Name.Equals("Sizes"));
    var generatedDotnetMauiAnimationsDir = generatedDotnetMauiDir.GetDirectories().FirstOrDefault(d => d.Name.Equals("Animations"));

    //The source repository paths

    var libraryResourcesDir = new DirectoryInfo(Path.Combine(LibraryDir, "Resources"));
    var libraryAndroidDir = new DirectoryInfo(Path.Combine(LibraryDir, "Platforms", "Android"));

    var libraryDotnetMauiColorsDir = libraryResourcesDir.GetDirectories().FirstOrDefault(d => d.Name.Equals("Colors"));
    var libraryDotnetMauiIconsDir = libraryResourcesDir.GetDirectories().FirstOrDefault(d => d.Name.Equals("Icons"));
    var libraryDotnetMauiSizesDir = libraryResourcesDir.GetDirectories().FirstOrDefault(d => d.Name.Equals("Sizes"));
    var libraryDotnetMauiAnimationsDir = libraryResourcesDir.GetDirectories().FirstOrDefault(d => d.Name.Equals("Animations"));


    //Copy to the correct folders in the branch
    //Colors
    generatedAndroidColorFile.CopyTo(Path.Combine(libraryAndroidDir.FullName, "Resources", "values", generatedAndroidColorFile.Name), true);
    DirectoryHelper.CopyDirectory(generatedDotnetMauiColorsDir.FullName, libraryDotnetMauiColorsDir.FullName, true, true);

    //Icons
    
    var oldFiles = Directory.GetFiles(libraryDotnetMauiIconsDir.FullName, "*.svg").Select(f => new FileInfo(f));
    var generatedFiles = Directory.GetFiles(generatedDotnetMauiIconsDir.FullName, "*.svg").Select(f => new FileInfo(f));
    //Get the newly added icons
    var newIcons = new List<FileInfo>();
    foreach (var generatedIcon in generatedFiles)
    {
        if(!oldFiles.Any(f => f.Name.Equals(generatedIcon.Name))){
            newIcons.Add(generatedIcon);
        }
    }

    //Get the deleted icons
    var deletedIcons = new List<FileInfo>();
    foreach (var oldFile in oldFiles)
    {
        if (!generatedFiles.Any(f => f.Name.Equals(oldFile.Name)))
        {
            deletedIcons.Add(oldFile);
        }
    }

    WriteToFileHelper.WriteToEnumFile(libraryDotnetMauiIconsDir.GetFiles().FirstOrDefault(f => f.Name.Equals("IconName.cs")).FullName
                                    , newIcons.Select(f => f.Name).ToArray(), (enumName =>
                                    {
                                        return $"///<summary><a href=\"https://raw.githubusercontent.com/DIPSAS/DIPS.Mobile.UI/main/src/library/DIPS.Mobile.UI/Resources/Icons/{enumName}.svg\">View the icon in the browser</a></summary>";
                                    }), deletedIcons.Select(f => f.Name).ToArray());
    // }

    // //Modify enum
    // var iconNameFile = libraryDotnetMauiIconsDir.GetFiles().FirstOrDefault(f => f.Name.Equals("IconName.cs"));
    // var iconNameContent = await File.ReadAllTextAsync(iconNameFile.FullName);
    // var enums = iconNameContent.Split(",");

    // var highestIndex = -1;
    // foreach (var theEnum in enums)
    // {
    //     var result = theEnum[(theEnum.LastIndexOf('=') + 1)..];
    //     if(int.TryParse(result, out var theEnumIndex))
    //     {
    //         if (theEnumIndex > highestIndex)
    //         {
    //             highestIndex = theEnumIndex;
    //         }
    //     }
    // }

    // foreach (var newFile in newFiles)
    // {
    //     var lastComma = iconNameContent.LastIndexOf(',');
    //     if (lastComma != -1)
    //     {
    //         var name = newFile.Name.Replace(".svg", "");
    //         var comment = $"///<summary><a href=\"https://raw.githubusercontent.com/DIPSAS/DIPS.Mobile.UI/main/src/library/DIPS.Mobile.UI/Resources/Icons/{newFile.Name}\">View the icon in the browser</a></summary>";
    //         highestIndex++;
    //         iconNameContent = iconNameContent.Remove(lastComma, 1).Insert(lastComma, $", \n{comment}\n{name}={highestIndex},");
    //     }
    // }

    // foreach (var deletedFile in deletedFiles)
    // {
    //     var name = deletedFile.Name.Replace(".svg", "");
    //     var lineThatContains = enums.FirstOrDefault(s => s.Contains(name));
    //     if(lineThatContains != null)
    //     {
    //         iconNameContent = iconNameContent.Replace(lineThatContains+",", "");
    //     }
    // }

    // Console.WriteLine(iconNameContent);
    // await File.WriteAllTextAsync(iconNameFile.FullName, iconNameContent);


    return;

    DirectoryHelper.CopyDirectory(generatedDotnetMauiIconsDir.FullName, libraryDotnetMauiIconsDir.FullName, true, true);
    //Sizes
    DirectoryHelper.CopyDirectory(generatedDotnetMauiSizesDir.FullName, libraryDotnetMauiSizesDir.FullName, true, true);
    //Animations
    DirectoryHelper.CopyDirectory(generatedDotnetMauiAnimationsDir.FullName, libraryDotnetMauiAnimationsDir.FullName, true, true);

    //Bump changelog
    var changesetMessage = "Resources was updated from DIPS.Mobile.DesignTokens";
    var latestVersion = new Version(VersionUtil.GetLatestVersionFromChangelog(ChangeLogPath));
    var nextVersion = new Version(latestVersion.Major, latestVersion.Minor + 1, 0);
    await FileHelper.PrependToFile(ChangeLogPath, $"## [{nextVersion}] \n- {changesetMessage}\n\n");

    //Commit changes
    Logger.LogDebug($"Resources moved to folders, commiting changes");
    await Command.CaptureAsync("git", "add .", RootDir);
    await Command.CaptureAsync("git", $"commit -m 'Generated'");

    Logger.LogDebug($"Pushing {prBranchName} to repository");
    await Command.CaptureAsync("git", $"push -f origin {prBranchName}");

    //Create PR
    await Command.CaptureAsync("gh", $"auth login");
    await Command.ExecuteAsync("gh", $"pr create --title \"{changesetMessage}\" --body \"Here is the latest resources that was generated by DIPS.Mobile.DesignTokens repository\"");
};

var args = Args;
if(args.Count() == 0){
    await ExecuteSteps(new string[]{"help"});
    WriteLine("Please select steps to run:");
    var input = ReadLine();
    args = input.Split(' ');
}

await ExecuteSteps(args);

async Task<FileInfo> PackLibrary(string outputdir = null)
{
    outputdir ??= OutputDir;
    var version = VersionUtil.GetLatestVersionFromChangelog(ChangeLogPath);
    if (!await Git.CurrentBranchIsMain())
    {
        var buildNumber = AzureDevops.GetEnvironmentVariable("Build.BuildNumber");
        version += $"-pre{buildNumber}";
    }

    await dotnet.Pack(LibraryProjectPath, version, outputdir);
    return FileHelper.FindSingleFileByExtension(outputdir, ".nupkg");;
}
