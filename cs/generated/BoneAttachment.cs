using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "bone_attachment")]
	public class BoneAttachment : Component
	{
		public BoneAttachment(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "bone_attachment" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getRelativePosition(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRelativePosition(IntPtr module, int cmp, Vec3 value);


		public Vec3 RelativePosition
		{
			get { return getRelativePosition(module_, entity_.entity_Id_); }
			set { setRelativePosition(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getRelativeRotation(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRelativeRotation(IntPtr module, int cmp, Vec3 value);


		public Vec3 RelativeRotation
		{
			get { return getRelativeRotation(module_, entity_.entity_Id_); }
			set { setRelativeRotation(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getBone(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setBone(IntPtr module, int cmp, int value);


		public int Bone
		{
			get { return getBone(module_, entity_.entity_Id_); }
			set { setBone(module_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
