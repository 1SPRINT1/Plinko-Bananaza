using System;
using System.IO;
using System.Linq;
using UnityEngine;

public static class VersionHelper {
    
    public static string BundleCode() {

        var session = Resources.Load<TextAsset>("bundle");

        if (session != null) {
            var data = session.text.Deserialize<BundleData>();
            return $"{data.version}";
        }

        return $"{Application.version}";
    }

    public static bool IsCurrentVersionOlder(string remoteVersion) {
        
        var localVersion = BundleCode();
        //Debug.Log($"<color=green>APP VERSION: {localVersion}, REMOTE: {remoteVersion}</color>");
        var local = localVersion.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
        var remote = remoteVersion.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

        try {
            for (var i = 0; i < Math.Min(local.Length, remote.Length); i++) {
                var l = local[i];
                var r = remote[i];
                //Debug.Log($"local{l} , remote {r}");
                if (l != r)
                    return l < r;
            }

            // if major versions are equals
            // e.g: versions comparison are 0.1 and 0.1.1
            if (local.Length < remote.Length)
                return true;
        } catch (Exception ex) {
            Debug.Log(ex);
        }

        return false;
    }
    
    private struct BundleData {
        public string version;
    }
    
    public static void CreateBundleData(string version, int bundleVersionCode) {
        var data = new BundleData() { version = $"{version}.{bundleVersionCode}"}.Serialize();

        var path = Path.Combine(Application.dataPath, "Resources", "bundle.json");
        
        if(File.Exists(path)) File.Delete(path);
        
        File.WriteAllText(path,data);
    }
}
