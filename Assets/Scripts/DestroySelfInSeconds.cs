using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfInSeconds : MonoBehaviour
{
    public float destroyTime = 1.0f;
    float currTime = 0.0f;

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;
        if(currTime > destroyTime)
        {
            Destroy(gameObject);
        }
    }
}
