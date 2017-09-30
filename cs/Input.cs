using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public static class Input	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool isMouseDown(Input.MouseButton button);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getActionValue(uint action);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getMouseXMove();


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getMouseYMove();


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec2 getMousePos();


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void addAction(uint action, Input.InputType type, int key, int controller_id);


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
			return isMouseDown(button);
		}

		public static float GetActionValue(uint action)
		{
			return getActionValue(action);
		}

		public static float MouseXMove
		{
			get
			{
				return getMouseXMove();
			}
		}

		public static float MouseYMove
		{
			get
			{
				return getMouseYMove();
			}
		}

		public static Vec2 MousePos
		{
			get
			{
				return getMousePos();
			}
		}

		public static void AddAction(uint action, Input.InputType type, int key, int controller_id)
		{
			addAction(action, type, key, controller_id);
		}

	}

}
