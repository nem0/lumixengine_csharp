using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "curve_decal")]
	public class CurveDecal : Component
	{
		public CurveDecal(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "curve_decal" )) { }


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
		extern static float getHalfExtents(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setHalfExtents(IntPtr module, int cmp, float value);


		public float HalfExtents
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

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getBezierP0(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setBezierP0(IntPtr module, int cmp, Vec2 value);


		public Vec2 BezierP0
		{
			get { return getBezierP0(module_, entity_.entity_Id_); }
			set { setBezierP0(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getBezierP2(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setBezierP2(IntPtr module, int cmp, Vec2 value);


		public Vec2 BezierP2
		{
			get { return getBezierP2(module_, entity_.entity_Id_); }
			set { setBezierP2(module_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
