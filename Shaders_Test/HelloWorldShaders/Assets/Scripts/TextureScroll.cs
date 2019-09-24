using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScroll : MonoBehaviour
{
	public float scrollSpeedX = 0.5F;
	public float scrollSpeedY = 0.5F;
	public Renderer rend;
	public string textureName = "";

	void Start()
	{
		rend = GetComponent<Renderer>();
	}
	void Update()
	{
		float offsetX = Time.time * scrollSpeedX;
		float offsetY = Time.time * scrollSpeedY;
		rend.material.SetTextureOffset(textureName, new Vector2(offsetX, offsetY));
	}
}