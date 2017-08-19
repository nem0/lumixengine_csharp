using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
 
public class Main : Lumix.Component
{
	float t = 0;
	
    public void startGame()
    {
		Lumix.Engine.logError("hello from c#" + entity.native);
    }
	
	public void update(float time_delta)
	{
		t += time_delta;
		entity.setPosition((float)Math.Cos(t) * 10, (float)Math.Sin(t) * 10, 0);
	}
}