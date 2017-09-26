using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class AnimController : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getControllerSource(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setControllerSource(IntPtr scene, int cmp, string value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getControllerInputIndex(IntPtr instance, int cmp, string name);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setControllerInput(IntPtr instance, int cmp, int input_idx, int value);
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setControllerInput(IntPtr instance, int cmp, int input_idx, float value);
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setControllerInput(IntPtr instance, int cmp, int input_idx, bool value);



		public static string GetCmpType{ get { return "anim_controller"; } }


		public AnimController(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_.instance_, "anim_controller");
		}

		public AnimController(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_.instance_, entity_.entity_Id_, "anim_controller");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_.instance_, "anim_controller");
		}

		/// <summary>
		/// Gets or sets the Source
		/// </summary>
		public string Source
		{
			get { return getControllerSource(scene_, componentId_); }
			set { setControllerSource(scene_, componentId_, value); }
		}

		public int GetControllerInputIndex(string name)
		{
			return getControllerInputIndex(scene_, componentId_, name);
		}

		public void SetControllerInput(int input_idx, int value)
		{
			setControllerInput(scene_, componentId_, input_idx, value);
		}

		public void SetControllerInput(int input_idx, float value)
		{
			setControllerInput(scene_, componentId_, input_idx, value);
		}

		public void SetControllerInput(int input_idx, bool value)
		{
			setControllerInput(scene_, componentId_, input_idx, value);
		}

	}//end class

}//end namespace
