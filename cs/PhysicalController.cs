using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent( Type = "physical_controller")]
	public class PhysicalController :Component
	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getControllerLayer(IntPtr scene, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setControllerLayer(IntPtr scene, int cmp, int value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void moveController(IntPtr instance, int cmp, Vec3 v);



		public static string GetCmpType{ get { return "physical_controller"; } }


		public PhysicsScene Scene
		{
			 get { return new PhysicsScene(scene_); }
		}
		/// <summary>
		/// Gets or sets the Layer
		/// </summary>
		public int Layer
		{
			get { return getControllerLayer(scene_, componentId_); }
			set { setControllerLayer(scene_, componentId_, value); }
		}

		public PhysicalController(Entity _entity, int _cmpId)
			: base(_entity, _cmpId, getScene(_entity.instance_, GetCmpType)) { }

		public void MoveController(Vec3 v)
		{
			moveController(scene_, componentId_, v);
		}

	}//end class

}//end namespace
