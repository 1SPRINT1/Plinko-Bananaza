using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Builder {
    public enum BuildAppTarget { Apk , Aab }
    private static string _pass = "123qwe";

    private static string[] GetActiveScenes() {
        var result = new List<string>();

        foreach (var scene in EditorBuildSettings.scenes)
            if (scene.enabled)
                result.Add(scene.path);

        return result.ToArray();
    }

    private static string AppRoot() {
        return Application.dataPath + "/../";
    }
    
    private static BuildPlayerOptions Create(BuildAppTarget app, BuildTarget target, BuildOptions options, string version = "") {
        var targetExtension = app == BuildAppTarget.Aab ? "aab" : "apk";
        
        return new BuildPlayerOptions {
            locationPathName = Path.Combine(Application.dataPath, $"../Builds/{Application.productName}-{Application.version}{version}-Install.{targetExtension}"),
            scenes = GetActiveScenes(),
            target = target,
            options = options,
        };
    }
    
    private static string GetAndroidBundleSubversion() => "." + PlayerSettings.Android.bundleVersionCode;
    
    [MenuItem("Builder/[Debug] Build APK Android")]
    public static void BuildDebugAndroidPlayer() {
        Build(BuildAppTarget.Apk, Create(BuildAppTarget.Apk, BuildTarget.Android, BuildOptions.ShowBuiltPlayer | BuildOptions.Development, GetAndroidBundleSubversion()));
    }

    [MenuItem("Builder/Build APK Android")]
    public static void BuildAndroidPlayer() {
        Build(BuildAppTarget.Apk, Create(BuildAppTarget.Apk, BuildTarget.Android, BuildOptions.ShowBuiltPlayer, GetAndroidBundleSubversion()));
    }
    
    [MenuItem("Builder/[Debug] Build AAB Android")]
    public static void BuildDebugAbbAndroidPlayer() {
        Build(BuildAppTarget.Aab, Create(BuildAppTarget.Aab, BuildTarget.Android, BuildOptions.ShowBuiltPlayer | BuildOptions.Development, GetAndroidBundleSubversion()));
    }

    [MenuItem("Builder/Build AAB Android")]
    public static void BuildAabAndroidPlayer() {
        Build(BuildAppTarget.Aab, Create(BuildAppTarget.Aab, BuildTarget.Android, BuildOptions.ShowBuiltPlayer, GetAndroidBundleSubversion()));
    }

    private static void Build(BuildAppTarget target, BuildPlayerOptions options) {
        VersionHelper.CreateBundleData(Application.version, PlayerSettings.Android.bundleVersionCode);
        
        PlayerSettings.keyaliasPass = _pass;
        PlayerSettings.keystorePass = _pass;
        
        EditorUserBuildSettings.buildAppBundle = target == BuildAppTarget.Aab;
        
        if(!KeyStoreFileExists()) Debug.Log($"Keystore not found!");
        
        BuildPipeline.BuildPlayer(options);
    }

    private static bool KeyStoreFileExists() {
        var projectPath = Path.Combine(Application.dataPath, "../user.keystore");
        return File.Exists(projectPath);
    }
}
