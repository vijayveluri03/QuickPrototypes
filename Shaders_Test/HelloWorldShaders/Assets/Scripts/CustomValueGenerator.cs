using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomValueGenerator : MonoBehaviour
{
	public float scrollSpeed = 0.5F;
	public Renderer rend;
	public float value = 0;
	public float minValue = 0;
	public float maxValue = 0;
	public bool ossilate = false;
	public int multiplier = 1;

	void Start()
	{
		rend = GetComponent<Renderer>();
		value = minValue;
	}
	void Update()
	{
		value = scrollSpeed * multiplier * Time.deltaTime + value;

		if ( ossilate == false )
		{
			if ( value > maxValue )
				value = minValue;
		}
		else 
		{
			if ( value > maxValue && multiplier > 0 )
				multiplier = -1;
			else if ( value < minValue && multiplier < 0 )
				multiplier = 1;
		}

		rend.material.SetFloat( "_CustomValue", value );
	}
}