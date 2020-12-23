using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearObject : MonoBehaviour
{
    // clear objects child object
    public void OnClick()
    {
        var obj = GameObject.Find("objects");
        foreach(Transform child in obj.transform)
        {
            Destroy(child.gameObject);
        }
    }
}

