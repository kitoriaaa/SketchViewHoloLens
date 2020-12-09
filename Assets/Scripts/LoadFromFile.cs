﻿using UnityEngine;
using System.Collections;
using System.IO;


public class LoadFromFile : MonoBehaviour
{
    IEnumerator Start()
    {
#if WINDOWS_UWP	
        string bundlepath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "object");
#else
        string bundlepath = Path.Combine(Application.streamingAssetsPath, "object");
#endif
        AssetBundleCreateRequest bundleLoadRequest = AssetBundle.LoadFromFileAsync(bundlepath);
        yield return bundleLoadRequest;

        AssetBundle myLoadedAssetBundle = bundleLoadRequest.assetBundle;
        if (myLoadedAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            yield break;
        }

        AssetBundleRequest assetLoadRequest = myLoadedAssetBundle.LoadAssetAsync<GameObject>("mesh");
        yield return assetLoadRequest;

        GameObject prefab = assetLoadRequest.asset as GameObject;

        // Add componet test move script
        prefab.AddComponent<MoveScript>();

        // Setting position
        Camera cam = Camera.main;
        prefab.transform.position = cam.WorldToViewportPoint(new Vector3(0f, 0f, 5f));

        Instantiate(prefab);

        myLoadedAssetBundle.Unload(false);
    }
}

