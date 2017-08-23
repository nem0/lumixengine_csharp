using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
 
public class Main : Lumix.Component
{
	float t = 0;
	
    public void startGame()
    {
		Lumix.Engine.logError("start game");
		var point_light = entity.createComponent<Lumix.PointLight>();
		point_light.Color = new Lumix.Vec3(1, 0, 0);
    }
	
	public void update(float time_delta)
	{
		t += time_delta;
		entity.position = new Lumix.Vec3((float)Math.Cos(t) * 10, (float)Math.Sin(t) * 10, 0);
	}
}