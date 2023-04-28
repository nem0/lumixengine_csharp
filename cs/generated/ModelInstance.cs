using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "model_instance")]
	public class ModelInstance : Component
	{
		public ModelInstance(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "model_instance" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool getEnabled(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setEnabled(IntPtr module, int cmp, bool value);


		public bool IsEnabled
		{
			get { return getEnabled(module_, entity_.entity_Id_); }
			set { setEnabled(module_, entity_.entity_Id_, value); }
		}

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
		extern static string getSource(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSource(IntPtr module, int cmp, string value);


		public string Source
		{
			get { return getSource(module_, entity_.entity_Id_); }
			set { setSource(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static IntPtr getModelInstanceModel(IntPtr instance, int cmp);

		public IntPtr GetModelInstanceModel()
		{
			return getModelInstanceModel(module_, entity_.entity_Id_);
		}

	} // class
} // namespace
