using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System.IO;
using System.Linq;

public static class BuildScript
{
    public static void BuildWebGL()
    {
        // Build into ./docs (GitHub Pages friendly)
        var buildPath = Path.GetFullPath("docs");
        if (!Directory.Exists(buildPath))
            Directory.CreateDirectory(buildPath);

        // Use scenes that are enabled in Build Settings
        var scenes = EditorBuildSettings.scenes
            .Where(s => s.enabled)
            .Select(s => s.path)
            .ToArray();

        if (scenes.Length == 0)
        {
            Debug.LogError("No scenes are enabled in File → Build Settings. Please add your main scene.");
            EditorApplication.Exit(1);
            return;
        }

        var report = BuildPipeline.BuildPlayer(scenes, buildPath, BuildTarget.WebGL, BuildOptions.None);

        if (report.summary.result != BuildResult.Succeeded)
        {
            Debug.LogError("WebGL build failed: " + report.summary.result);
            EditorApplication.Exit(1);
            return;
        }

        Debug.Log("WebGL build succeeded! Output: " + buildPath);
    }
}
