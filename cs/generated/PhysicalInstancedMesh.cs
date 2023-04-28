using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "physical_instanced_mesh")]
	public class PhysicalInstancedMesh : Component
	{
		public PhysicalInstancedMesh(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "physical_instanced_mesh" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getMesh(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setMesh(IntPtr module, int cmp, string value);


		public string Mesh
		{
			get { return getMesh(module_, entity_.entity_Id_); }
			set { setMesh(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getLayer(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLayer(IntPtr module, int cmp, int value);


		public int Layer
		{
			get { return getLayer(module_, entity_.entity_Id_); }
			set { setLayer(module_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
