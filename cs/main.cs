using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
 
public class Main : Lumix.Component
{
	float t = 0;
	
    public void startGame()
    {
		Lumix.Engine.logError("start game");
		var animable = entity.createComponent<Lumix.Animable>();
		animable.source = "Abc";
    }
	
	public void update(float time_delta)
	{
		t += time_delta;
		entity.position = new Lumix.Vector3((float)Math.Cos(t) * 10, (float)Math.Sin(t) * 10, 0);
	}
}