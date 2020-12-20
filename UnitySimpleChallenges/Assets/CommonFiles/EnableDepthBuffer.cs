using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class EnableDepthBuffer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Camera>().depthTextureMode = DepthTextureMode.Depth;
        Debug.Log("Depth is set");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
