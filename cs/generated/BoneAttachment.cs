using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "bone_attachment")]
	public class BoneAttachment : Component
	{
		public BoneAttachment(Entity _entity)
			: base(_entity,  getScene(_entity.instance_, "bone_attachment" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getRelativePosition(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRelativePosition(IntPtr scene, int cmp, Vec3 value);


		public Vec3 RelativePosition
		{
			get { return getRelativePosition(scene_, entity_.entity_Id_); }
			set { setRelativePosition(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getRelativeRotation(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRelativeRotation(IntPtr scene, int cmp, Vec3 value);


		public Vec3 RelativeRotation
		{
			get { return getRelativeRotation(scene_, entity_.entity_Id_); }
			set { setRelativeRotation(scene_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getBone(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setBone(IntPtr scene, int cmp, int value);


		public int Bone
		{
			get { return getBone(scene_, entity_.entity_Id_); }
			set { setBone(scene_, entity_.entity_Id_, value); }
		}

	} // class
} // namespace
