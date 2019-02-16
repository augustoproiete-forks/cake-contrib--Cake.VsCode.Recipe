///////////////////////////////////////////////////////////////////////////////
// ADDINS
///////////////////////////////////////////////////////////////////////////////

#addin nuget:?package=MagicChunks&version=2.0.0.119
#addin nuget:?package=Cake.Figlet&version=1.2.0
#addin nuget:?package=Cake.Git&version=0.19.0
#addin nuget:?package=Cake.Gitter&version=0.10.0
#addin nuget:?package=Cake.Graph&version=0.6.0
#addin nuget:?package=Cake.Incubator&version=3.1.0
#addin nuget:?package=Cake.Kudu&version=0.8.0
#addin nuget:?package=Cake.MicrosoftTeams&version=0.8.0
#addin nuget:?package=Cake.Npm&version=0.10.0
#addin nuget:?package=Cake.Slack&version=0.12.0
#addin nuget:?package=Cake.Twitter&version=0.9.0
#addin nuget:?package=Cake.VsCode&version=0.8.0
#addin nuget:?package=Cake.Wyam&version=1.7.4
// Needed for Cake.Graph
#addin nuget:?package=RazorEngine&version=3.10.0&loaddependencies=true

Action<string, IDictionary<string, string>> RequireAddin = (code, envVars) => {
    var script = MakeAbsolute(File(string.Format("./{0}.cake", Guid.NewGuid())));
    try
    {
        System.IO.File.WriteAllText(script.FullPath, code);
        var arguments = new Dictionary<string, string>();

        if(BuildParameters.CakeConfiguration.GetValue("NuGet_UseInProcessClient") != null) {
            arguments.Add("nuget_useinprocessclient", BuildParameters.CakeConfiguration.GetValue("NuGet_UseInProcessClient"));
        }

        if(BuildParameters.CakeConfiguration.GetValue("Settings_SkipVerification") != null) {
            arguments.Add("settings_skipverification", BuildParameters.CakeConfiguration.GetValue("Settings_SkipVerification"));
        }

        CakeExecuteScript(script,
            new CakeSettings
            {
                EnvironmentVariables = envVars,
                Arguments = arguments
            });
    }
    finally
    {
        if (FileExists(script))
        {
            DeleteFile(script);
        }
    }
};
