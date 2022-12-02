using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "decal")]
	public class Decal : Component
	{
		public Decal(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "decal" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getMaterial(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setMaterial(IntPtr scene, int cmp, string value);


		public string Material
		{
			get { return getMaterial(scene_, entity_.entity_Id_); }
			set { setMaterial(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getHalfExtents(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setHalfExtents(IntPtr scene, int cmp, Vec3 value);


		public Vec3 HalfExtents
		{
			get { return getHalfExtents(scene_, entity_.entity_Id_); }
			set { setHalfExtents(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getUVScale(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setUVScale(IntPtr scene, int cmp, Vec2 value);


		public Vec2 UVScale
		{
			get { return getUVScale(scene_, entity_.entity_Id_); }
			set { setUVScale(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
