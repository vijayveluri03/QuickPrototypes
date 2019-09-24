using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapTexture : MonoBehaviour 
{
	public Renderer rendererr;
	public string propertyName;
	public float startValue;
	public float endValue;
	public float speed;

	private float currValue;
	// Use this for initialization
	void Start () 
	{
		currValue = startValue;
	}
	
	// Update is called once per frame
	void Update () 
	{
		currValue += speed * Time.deltaTime;
		rendererr.material.SetFloat ( propertyName, currValue );
	}
}
