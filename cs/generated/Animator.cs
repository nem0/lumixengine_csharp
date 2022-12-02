using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "animator")]
	public class Animator : Component
	{
		public Animator(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "animator" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getSource(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSource(IntPtr scene, int cmp, string value);


		public string Source
		{
			get { return getSource(scene_, entity_.entity_Id_); }
			set { setSource(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static uint getDefaultSet(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setDefaultSet(IntPtr scene, int cmp, uint value);


		public uint DefaultSet
		{
			get { return getDefaultSet(scene_, entity_.entity_Id_); }
			set { setDefaultSet(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
