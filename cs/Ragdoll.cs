using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class Ragdoll : NativeComponent
	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getRagdollLayer(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRagdollLayer(IntPtr scene, int cmp, int value);


		public static string GetCmpType{ get { return "ragdoll"; } }


		public Ragdoll(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_.instance_, "ragdoll");
		}

		public Ragdoll(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_.instance_, entity_.entity_Id_, "ragdoll");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_.instance_, "ragdoll");
		}

		/// <summary>
		/// Gets or sets the Layer
		/// </summary>
		public int Layer
		{
			get { return getRagdollLayer(scene_, componentId_); }
			set { setRagdollLayer(scene_, componentId_, value); }
		}

	}//end class

}//end namespace
