using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "spline")]
	public class Spline : Component
	{
		public Spline(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "spline" )) { }


	} // class
} // namespace
