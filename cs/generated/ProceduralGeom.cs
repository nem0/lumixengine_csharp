using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "procedural_geom")]
	public class ProceduralGeom : Component
	{
		public ProceduralGeom(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "procedural_geom" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getMaterial(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setMaterial(IntPtr module, int cmp, string value);


		public string Material
		{
			get { return getMaterial(module_, entity_.entity_Id_); }
			set { setMaterial(module_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
