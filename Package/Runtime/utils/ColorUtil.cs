using System;
using UnityEngine;


public static class ColorUtil
{
    public const int SKY_BLUE = 0x87CEEB;
    public const int LIGHT_GREEN = 0x90EE90;
    public const int ORANGE = 0xFFA500;
    public static Color RandomColor()
	{
		float r = UnityEngine.Random.Range(0f,1f);
		float g = UnityEngine.Random.Range(0f,1f);
		float b = UnityEngine.Random.Range(0f,1f);
		Color color = new(r,g,b);
		return color;
	}
}


