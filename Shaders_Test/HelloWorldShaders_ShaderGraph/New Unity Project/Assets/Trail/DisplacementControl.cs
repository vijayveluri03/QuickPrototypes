using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplacementControl : MonoBehaviour
{
    public float displacementAmount;
    SkinnedMeshRenderer skinnedMeshRenderer;
    MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        displacementAmount = Mathf.Lerp(displacementAmount, 0, Time.deltaTime );
        if ( meshRenderer != null )
            meshRenderer.sharedMaterial.SetFloat("_NoiseAmplitude", displacementAmount);
        if ( skinnedMeshRenderer != null )
            skinnedMeshRenderer.sharedMaterial.SetFloat("_NoiseAmplitude", displacementAmount);

        if ( Input.GetMouseButtonDown(0))
        {
            displacementAmount += 1;
        }
    }
}
