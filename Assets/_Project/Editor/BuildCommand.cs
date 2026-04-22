using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Reactor.Editor
{
    public static class BuildCommand
    {
        public static void PerformBuild()
        {
            Debug.Log("Starting CI Build...");

            string[] defaultScenes = EditorBuildSettings.scenes
                .Where(s => s.enabled)
                .Select(s => s.path)
                .ToArray();

            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
            {
                scenes = defaultScenes,
                locationPathName = "build/Android/ReactorAR.apk",
                target = BuildTarget.Android,
                options = BuildOptions.None
            };

            BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            BuildSummary summary = report.summary;

            if (summary.result == BuildResult.Succeeded)
            {
                Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
            }

            if (summary.result == BuildResult.Failed)
            {
                Debug.LogError("Build failed");
                EditorApplication.Exit(1);
            }
        }
    }
}
