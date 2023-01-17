#load "BuildSystem/Steps.csx"
#load "BuildSystem/Build/Xamarin/Android.csx"
#load "BuildSystem/Build/Xamarin/iOS.csx"
#load "BuildSystem/Build/MSBuild.csx"
#load "BuildSystem/Repository.csx"
#load "BuildSystem/Command.csx"
#load "BuildSystem/Nuget.csx"

private static string RootDir = Repository.RootDir();
private static string SrcDir = Path.Combine(RootDir, "src");
//Full solution with library and samples app
private static string SolutionPath = SrcDir;
private static string LibraryAndroidPath = Path.Combine(RootDir, "src","library","DIPS.Mobile.UI.Droid");
private static string LibraryiOSPath = Path.Combine(RootDir, "src", "library", "DIPS.Mobile.UI.iOS");
//Source generator paths
private static string SourceGeneratorPath = Path.Combine(SrcDir, "sourcegenerator", "DIPS.Mobile.UI.SourceGenerator");
//Solution with nuget tests to test the nuget package
private static string NugetTestSolutionPath = Path.Combine(RootDir, "src", "tests", "nugettest");
private static string NugetTestAndroidPath = Path.Combine(RootDir, "src", "tests", "nugettest", "NugetTest.Droid");
private static string NugetTestiOSPath = Path.Combine(RootDir, "src", "tests", "nugettest", "NugetTest.iOS");

//Nuget pa

AsyncStep nugetTest = async () =>
{
    //Clear from cache
    var homePath = Environment.GetEnvironmentVariable("HOME");
    File.Delete(Path.Combine(homePath, ".nuget", "packages", "dips.mobile.ui"));
    var files = Directory.EnumerateFiles(Path.Combine(NugetTestSolutionPath, "packages"));
    foreach (var file in files)
    {
        File.Delete(file);
    }

    await MSBuild.Build(SourceGeneratorPath);
    await Android.Build(LibraryAndroidPath);
    await iOS.Build(LibraryiOSPath);
    await Nuget.Pack(RootDir, Path.Combine(NugetTestSolutionPath, "packages"));
};

Step step2 = () => WriteLine("Build ran!");

var args = new string[] { "nugettest" };
if(args.Count() == 0){
    WriteLine("Please select steps to run:");
    var input = ReadLine();
    args = input.Split(' ');
}

await ExecuteSteps(args);
