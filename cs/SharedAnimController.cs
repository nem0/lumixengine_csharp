using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class SharedAnimController : NativeComponent
	{
		int componentId_;
		IntPtr scene_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Entity getSharedControllerParent(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSharedControllerParent(IntPtr scene, int cmp, Entity value);


		public static string GetCmpType{ get { return "shared_anim_controller"; } }


		public SharedAnimController(Entity _entity, int _componenId)
		{
			entity_ = _entity;
			componentId_ = _componenId;
			scene_ = getScene(entity_._universe, "shared_anim_controller");
		}

		public SharedAnimController(Entity _entity)
		{
			entity_ = _entity;
			componentId_ = create(entity_._universe, entity_._entity_id, "shared_anim_controller");
			if (componentId_ < 0) throw new Exception("Failed to create component");
			scene_ = getScene(entity_._universe, "shared_anim_controller");
		}

		/// <summary>
		/// Gets or sets the Parent
		/// </summary>
		public Entity Parent
		{
			get { return getSharedControllerParent(scene_, componentId_); }
			set { setSharedControllerParent(scene_, componentId_, value); }
		}

	}//end class

}//end namespace
