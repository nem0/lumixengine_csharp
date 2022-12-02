using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "instanced_model")]
	public class InstancedModel : Component
	{
		public InstancedModel(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "instanced_model" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getModel(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setModel(IntPtr scene, int cmp, string value);


		public string Model
		{
			get { return getModel(scene_, entity_.entity_Id_); }
			set { setModel(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
