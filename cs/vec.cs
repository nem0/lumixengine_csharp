using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace Lumix
{


public struct Vec4
{
    public float x;
    public float y;
	public float z;
	public float w;
	
	public Vec4(float _x, float _y, float _z, float _w)
	{
		x = _x;
		y = _y;
		z = _z;
		w = _w;
	}
}


public struct Vec3
{
    public float x;
    public float y;
	public float z;
	
	public Vec3(float _x, float _y, float _z)
	{
		x = _x;
		y = _y;
		z = _z;
	}
}

public struct Vec2
{
    public float x;
    public float y;
	
	public Vec2(float _x, float _y)
	{
		x = _x;
		y = _y;
	}
}


public struct Int2
{
    public int x;
    public int y;
	
	public Int2(int _x, int _y)
	{
		x = _x;
		y = _y;
	}
}
}
