using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace Lumix
{


	public class Camera : NativeComponent
	{
		private int component_id;
		private IntPtr scene;

		public static string GetCmpType() { return "camera"; }

		public Camera(Entity _entity, int _component_id)
		{
			entity = _entity;
			component_id = _component_id;
			scene = getScene(entity._universe, "camera");
		}

		public Camera(Entity entity)
		{
			component_id = create(entity._universe, entity._entity_id, "camera");
			if (component_id < 0) throw new Exception("Failed to create component");
			scene = getScene(entity._universe, "camera");
		}


		/* FOV */
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static void setFOV(IntPtr scene, int cmp, float source);
		
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static float getFOV(IntPtr scene, int cmp);
		
		public float FOV
		{
			get{ return getFOV(scene, component_id); }
			set{ setFOV(scene, component_id, value); }
		}


		/* OrthoSize */
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static void setOrthoSize(IntPtr scene, int cmp, float source);
		
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static float getOrthoSize(IntPtr scene, int cmp);
		
		public float OrthoSize
		{
			get{ return getOrthoSize(scene, component_id); }
			set{ setOrthoSize(scene, component_id, value); }
		}


		/* Slot */
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static void setSlot(IntPtr scene, int cmp, string source);
		
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static string getSlot(IntPtr scene, int cmp);
		
		public string Slot
		{
			get{ return getSlot(scene, component_id); }
			set{ setSlot(scene, component_id, value); }
		}


		/* FarPlane */
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static void setFarPlane(IntPtr scene, int cmp, float source);
		
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static float getFarPlane(IntPtr scene, int cmp);
		
		public float FarPlane
		{
			get{ return getFarPlane(scene, component_id); }
			set{ setFarPlane(scene, component_id, value); }
		}


		/* NearPlane */
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static void setNearPlane(IntPtr scene, int cmp, float source);
		
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static float getNearPlane(IntPtr scene, int cmp);
		
		public float NearPlane
		{
			get{ return getNearPlane(scene, component_id); }
			set{ setNearPlane(scene, component_id, value); }
		}


	}

}
