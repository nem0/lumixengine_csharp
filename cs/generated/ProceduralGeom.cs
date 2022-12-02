using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "procedural_geom")]
	public class ProceduralGeom : Component
	{
		public ProceduralGeom(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "procedural_geom" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getMaterial(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setMaterial(IntPtr scene, int cmp, string value);


		public string Material
		{
			get { return getMaterial(scene_, entity_.entity_Id_); }
			set { setMaterial(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
