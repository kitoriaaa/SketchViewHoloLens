// test script

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour
{
    float sign = 1f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float xpos = transform.position.x;
        if (xpos < -8f || xpos > 8f) 
            sign *= -1f;
        Vector3 mv = new Vector3(0.1f * sign, 0f, 0f);
        transform.Translate(mv);
    }
}
