using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "fur")]
	public class Fur : Component
	{
		public Fur(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "fur" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static uint getLayers(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLayers(IntPtr scene, int cmp, uint value);


		public uint Layers
		{
			get { return getLayers(scene_, entity_.entity_Id_); }
			set { setLayers(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getScale(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setScale(IntPtr scene, int cmp, float value);


		public float Scale
		{
			get { return getScale(scene_, entity_.entity_Id_); }
			set { setScale(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getGravity(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setGravity(IntPtr scene, int cmp, float value);


		public float Gravity
		{
			get { return getGravity(scene_, entity_.entity_Id_); }
			set { setGravity(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getEnabled(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setEnabled(IntPtr scene, int cmp, bool value);


		public bool IsEnabled
		{
			get { return getEnabled(scene_, entity_.entity_Id_); }
			set { setEnabled(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
