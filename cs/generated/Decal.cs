using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "decal")]
	public class Decal : Component
	{
		public Decal(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "decal" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getMaterial(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setMaterial(IntPtr module, int cmp, string value);


		public string Material
		{
			get { return getMaterial(module_, entity_.entity_Id_); }
			set { setMaterial(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getHalfExtents(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setHalfExtents(IntPtr module, int cmp, Vec3 value);


		public Vec3 HalfExtents
		{
			get { return getHalfExtents(module_, entity_.entity_Id_); }
			set { setHalfExtents(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getUVScale(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setUVScale(IntPtr module, int cmp, Vec2 value);


		public Vec2 UVScale
		{
			get { return getUVScale(module_, entity_.entity_Id_); }
			set { setUVScale(module_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
