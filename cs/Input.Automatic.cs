using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public partial class Input	{
		public static IntPtr instance_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool isMouseDown(IntPtr instance, Input.MouseButton button);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getActionValue(IntPtr instance, uint action);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getMouseXMove(IntPtr instance);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getMouseYMove(IntPtr instance);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getMousePos(IntPtr instance);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void addAction(IntPtr instance, uint action, Input.InputType type, int key, int controller_id);


		public enum InputType
		{
			PRESSED,
			DOWN,
			MOUSE_X,
			MOUSE_Y,
			LTHUMB_X,
			LTHUMB_Y,
			RTHUMB_X,
			RTHUMB_Y,
			RTRIGGER,
			LTRIGGER,
		}

		public enum MouseButton
		{
			LEFT,
			MIDDLE,
			RIGHT,
		}

		public static bool IsMouseDown(Input.MouseButton button)
		{
			return isMouseDown(instance_, button);
		}

		public static float GetActionValue(uint action)
		{
			return getActionValue(instance_, action);
		}

		public static float MouseXMove
		{
			get
			{
				return getMouseXMove(instance_);
			}
		}

		public static float MouseYMove
		{
			get
			{
				return getMouseYMove(instance_);
			}
		}

		public static Vec2 MousePos
		{
			get
			{
				return getMousePos(instance_);
			}
		}

		public static void AddAction(uint action, Input.InputType type, int key, int controller_id)
		{
			addAction(instance_, action, type, key, controller_id);
		}

		public static implicit operator System.IntPtr(Input _value)
		{
			 return instance_;
		}
	}

}
