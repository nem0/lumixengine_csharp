using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "curve_decal")]
	public class CurveDecal : Component
	{
		public CurveDecal(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "curve_decal" )) { }


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
		extern static float getHalfExtents(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setHalfExtents(IntPtr scene, int cmp, float value);


		public float HalfExtents
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

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getBezierP0(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setBezierP0(IntPtr scene, int cmp, Vec2 value);


		public Vec2 BezierP0
		{
			get { return getBezierP0(scene_, entity_.entity_Id_); }
			set { setBezierP0(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getBezierP2(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setBezierP2(IntPtr scene, int cmp, Vec2 value);


		public Vec2 BezierP2
		{
			get { return getBezierP2(scene_, entity_.entity_Id_); }
			set { setBezierP2(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
