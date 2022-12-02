using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "gui_rect")]
	public class GuiRect : Component
	{
		public GuiRect(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "gui_rect" )) { }


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
		extern static bool getClipContent(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setClipContent(IntPtr scene, int cmp, bool value);


		public bool IsClipContent
		{
			get { return getClipContent(scene_, entity_.entity_Id_); }
			set { setClipContent(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getTopPoints(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setTopPoints(IntPtr scene, int cmp, float value);


		public float TopPoints
		{
			get { return getTopPoints(scene_, entity_.entity_Id_); }
			set { setTopPoints(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getTopRelative(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setTopRelative(IntPtr scene, int cmp, float value);


		public float TopRelative
		{
			get { return getTopRelative(scene_, entity_.entity_Id_); }
			set { setTopRelative(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getRightPoints(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRightPoints(IntPtr scene, int cmp, float value);


		public float RightPoints
		{
			get { return getRightPoints(scene_, entity_.entity_Id_); }
			set { setRightPoints(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getRightRelative(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRightRelative(IntPtr scene, int cmp, float value);


		public float RightRelative
		{
			get { return getRightRelative(scene_, entity_.entity_Id_); }
			set { setRightRelative(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getBottomPoints(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setBottomPoints(IntPtr scene, int cmp, float value);


		public float BottomPoints
		{
			get { return getBottomPoints(scene_, entity_.entity_Id_); }
			set { setBottomPoints(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getBottomRelative(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setBottomRelative(IntPtr scene, int cmp, float value);


		public float BottomRelative
		{
			get { return getBottomRelative(scene_, entity_.entity_Id_); }
			set { setBottomRelative(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getLeftPoints(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLeftPoints(IntPtr scene, int cmp, float value);


		public float LeftPoints
		{
			get { return getLeftPoints(scene_, entity_.entity_Id_); }
			set { setLeftPoints(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static float getLeftRelative(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setLeftRelative(IntPtr scene, int cmp, float value);


		public float LeftRelative
		{
			get { return getLeftRelative(scene_, entity_.entity_Id_); }
			set { setLeftRelative(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
