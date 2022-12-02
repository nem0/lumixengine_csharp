using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "physical_instanced_mesh")]
	public class PhysicalInstancedMesh : Component
	{
		public PhysicalInstancedMesh(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "physical_instanced_mesh" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getMesh(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setMesh(IntPtr scene, int cmp, string value);


		public string Mesh
		{
			get { return getMesh(scene_, entity_.entity_Id_); }
			set { setMesh(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getLayer(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLayer(IntPtr scene, int cmp, int value);


		public int Layer
		{
			get { return getLayer(scene_, entity_.entity_Id_); }
			set { setLayer(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
