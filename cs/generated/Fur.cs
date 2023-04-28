using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "fur")]
	public class Fur : Component
	{
		public Fur(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "fur" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static uint getLayers(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLayers(IntPtr module, int cmp, uint value);


		public uint Layers
		{
			get { return getLayers(module_, entity_.entity_Id_); }
			set { setLayers(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getScale(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setScale(IntPtr module, int cmp, float value);


		public float Scale
		{
			get { return getScale(module_, entity_.entity_Id_); }
			set { setScale(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getGravity(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setGravity(IntPtr module, int cmp, float value);


		public float Gravity
		{
			get { return getGravity(module_, entity_.entity_Id_); }
			set { setGravity(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getEnabled(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setEnabled(IntPtr module, int cmp, bool value);


		public bool IsEnabled
		{
			get { return getEnabled(module_, entity_.entity_Id_); }
			set { setEnabled(module_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
