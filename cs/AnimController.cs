using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class AnimController : NativeComponent
	{
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


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getControllerEntity(IntPtr instance, int cmp);



		public static string GetCmpType{ get { return "anim_controller"; } }


		/// <summary>
		/// Gets or sets the Source
		/// </summary>
		public string Source
		{
			get { return getControllerSource(scene_, componentId_); }
			set { setControllerSource(scene_, componentId_, value); }
		}

		public AnimController(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, GetCmpType)) { }

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

		public Entity GetControllerEntity()
		{
			int x = getControllerEntity(scene_, componentId_);
			 if(x < 0) return null;
			return new Entity(entity_.instance_, x);
		}

	}//end class

}//end namespace
