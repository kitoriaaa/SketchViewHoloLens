﻿using UnityEngine;
using System.Collections;
using System.IO;
using Microsoft.MixedReality.Toolkit.UI.BoundsControl;
using Microsoft.MixedReality.Toolkit.UI;

public class ViewButton : MonoBehaviour
{
    public GameObject Objects;

    public IEnumerator View()
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
        yield return assetLoadRequest; // AssetBundleの非同期読み込みが終わるまで中断


        // Create parents object
        Objects = GameObject.Find("objects");
        if (Objects == null)
        {
            Objects = new GameObject("objects");
        }


        GameObject prefab = assetLoadRequest.asset as GameObject;

        // Add componet test move script
        prefab.AddComponent<BoxCollider>();
        prefab.AddComponent<ManipulationHandler>();
        prefab.AddComponent<BoundsControl>();

        // Setting position
        Camera cam = Camera.main;
        prefab.transform.position = cam.transform.position + cam.transform.forward * 2;


        // Setting scale
        prefab.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

        var obj = Instantiate(prefab);
        // setting parents
        obj.transform.parent = Objects.transform;

        myLoadedAssetBundle.Unload(false);
    }

    
    public void OnClick()
    {
        //var obj = GameObject.Find("mesh(Clone)");
        //if (obj != null)
        //{
        //    Destroy(obj);
        //}
        StartCoroutine(View());
    }
}

