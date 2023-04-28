using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "animable")]
	public class Animable : Component
	{
		public Animable(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "animable" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getAnimation(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAnimation(IntPtr module, int cmp, string value);


		public string Animation
		{
			get { return getAnimation(module_, entity_.entity_Id_); }
			set { setAnimation(module_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
