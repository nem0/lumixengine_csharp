using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "gui_rect")]
	public class GuiRect : Component
	{
		public GuiRect(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "gui_rect" )) { }


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
		extern static bool getClipContent(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setClipContent(IntPtr module, int cmp, bool value);


		public bool IsClipContent
		{
			get { return getClipContent(module_, entity_.entity_Id_); }
			set { setClipContent(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getTopPoints(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setTopPoints(IntPtr module, int cmp, float value);


		public float TopPoints
		{
			get { return getTopPoints(module_, entity_.entity_Id_); }
			set { setTopPoints(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getTopRelative(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setTopRelative(IntPtr module, int cmp, float value);


		public float TopRelative
		{
			get { return getTopRelative(module_, entity_.entity_Id_); }
			set { setTopRelative(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getRightPoints(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRightPoints(IntPtr module, int cmp, float value);


		public float RightPoints
		{
			get { return getRightPoints(module_, entity_.entity_Id_); }
			set { setRightPoints(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getRightRelative(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRightRelative(IntPtr module, int cmp, float value);


		public float RightRelative
		{
			get { return getRightRelative(module_, entity_.entity_Id_); }
			set { setRightRelative(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getBottomPoints(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setBottomPoints(IntPtr module, int cmp, float value);


		public float BottomPoints
		{
			get { return getBottomPoints(module_, entity_.entity_Id_); }
			set { setBottomPoints(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getBottomRelative(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setBottomRelative(IntPtr module, int cmp, float value);


		public float BottomRelative
		{
			get { return getBottomRelative(module_, entity_.entity_Id_); }
			set { setBottomRelative(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getLeftPoints(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLeftPoints(IntPtr module, int cmp, float value);


		public float LeftPoints
		{
			get { return getLeftPoints(module_, entity_.entity_Id_); }
			set { setLeftPoints(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getLeftRelative(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLeftRelative(IntPtr module, int cmp, float value);


		public float LeftRelative
		{
			get { return getLeftRelative(module_, entity_.entity_Id_); }
			set { setLeftRelative(module_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
