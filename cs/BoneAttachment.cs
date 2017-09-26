using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class BoneAttachment : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Entity getBoneAttachmentParent(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setBoneAttachmentParent(IntPtr scene, int cmp, Entity value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Vec3 getBoneAttachmentPosition(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setBoneAttachmentPosition(IntPtr scene, int cmp, Vec3 value);


		public static string GetCmpType{ get { return "bone_attachment"; } }


		public BoneAttachment(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_.instance_, "bone_attachment");
		}

		public BoneAttachment(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_.instance_, entity_.entity_Id_, "bone_attachment");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_.instance_, "bone_attachment");
		}

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

	}//end class

}//end namespace
