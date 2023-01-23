using System;
using UnityEngine;


public class ColorUtil
{
	public ColorUtil ()
	{
	}

	public static Color RandomColor()
	{
		float r = UnityEngine.Random.Range(0f,1f);
		float g = UnityEngine.Random.Range(0f,1f);
		float b = UnityEngine.Random.Range(0f,1f);
		Color color = new Color(r,g,b);
		return color;
	}
}


