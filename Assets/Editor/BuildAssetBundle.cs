using UnityEditor;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class BuildAssetBundle
{
    [MenuItem("Assets/Build Obj")]
    static void ObjBuild()
    {
        var builds = new List<AssetBundleBuild>();

        var build = new AssetBundleBuild();
        build.assetBundleName = "object";
        build.assetNames = new string[1] { "Assets/AssetBundleResources/Object/mesh.obj" };

        builds.Add(build);

        var targetDir = "Assets/AssetBundles";
        if (!Directory.Exists(targetDir)) Directory.CreateDirectory(targetDir);


        BuildPipeline.BuildAssetBundles(targetDir, builds.ToArray(), BuildAssetBundleOptions.None, BuildTarget.WSAPlayer);
    }
}

