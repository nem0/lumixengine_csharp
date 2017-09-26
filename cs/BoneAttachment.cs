using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class BoneAttachment : NativeComponent
	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Entity getBoneAttachmentParent(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setBoneAttachmentParent(IntPtr scene, int cmp, Entity value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getBoneAttachmentPosition(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setBoneAttachmentPosition(IntPtr scene, int cmp, Vec3 value);


		public static string GetCmpType{ get { return "bone_attachment"; } }


		/// <summary>
		/// Gets or sets the Parent
		/// </summary>
		public Entity Parent
		{
			get { return getBoneAttachmentParent(scene_, componentId_); }
			set { setBoneAttachmentParent(scene_, componentId_, value); }
		}

		/// <summary>
		/// Gets or sets the RelativePosition
		/// </summary>
		public Vec3 RelativePosition
		{
			get { return getBoneAttachmentPosition(scene_, componentId_); }
			set { setBoneAttachmentPosition(scene_, componentId_, value); }
		}

		public BoneAttachment(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, GetCmpType)) { }

	}//end class

}//end namespace
