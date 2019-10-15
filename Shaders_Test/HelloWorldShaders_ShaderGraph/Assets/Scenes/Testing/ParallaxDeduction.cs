using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxDeduction : MonoBehaviour
{
    // This should be center and its forward should be the forward of the frame
    public Transform centerOfTheParallaxFrame;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Camera cam = GetPrimaryCamera();
        if ( cam == null )
            return;
        
        Vector3 cameraDirection = (centerOfTheParallaxFrame.position - cam.transform.position).normalized;
        Vector3 frameRight = centerOfTheParallaxFrame.right.normalized;

        float dotProductX = Vector3.Dot( centerOfTheParallaxFrame.right.normalized, -cameraDirection );
        float dotProductY = Vector3.Dot( centerOfTheParallaxFrame.up.normalized, -cameraDirection );
        Debug.Log( dotProductX );
        
    }


    Camera GetPrimaryCamera ()
    {
        // @todo
        return Camera.main;
    }


}
