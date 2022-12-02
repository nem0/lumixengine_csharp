using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "model_instance")]
	public class ModelInstance : Component
	{
		public ModelInstance(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "model_instance" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getEnabled(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setEnabled(IntPtr scene, int cmp, bool value);


		public bool IsEnabled
		{
			get { return getEnabled(scene_, entity_.entity_Id_); }
			set { setEnabled(scene_, entity_.entity_Id_, value); }
		}

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
		extern static string getSource(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSource(IntPtr scene, int cmp, string value);


		public string Source
		{
			get { return getSource(scene_, entity_.entity_Id_); }
			set { setSource(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
