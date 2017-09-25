using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public class Universe
	{
		IntPtr instance_;

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRotation(IntPtr instance, System.IntPtr entity, float x, float y, float z, float w);
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setRotation(IntPtr instance, System.IntPtr entity, System.IntPtr rot);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setPosition(IntPtr instance, System.IntPtr entity, float x, float y, float z);
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setPosition(IntPtr instance, System.IntPtr entity, Vec3 pos);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getPosition(IntPtr instance, System.IntPtr entity);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getRotation(IntPtr instance, System.IntPtr entity);


		internal Universe(IntPtr _instance)
		{
			instance_ = _instance;
		}

		public void SetRotation(System.IntPtr entity, float x, float y, float z, float w)
		{
			setRotation(instance_, entity, x, y, z, w);
		}

		public void SetRotation(System.IntPtr entity, System.IntPtr rot)
		{
			setRotation(instance_, entity, rot);
		}

		public void SetPosition(System.IntPtr entity, float x, float y, float z)
		{
			setPosition(instance_, entity, x, y, z);
		}

		public void SetPosition(System.IntPtr entity, Vec3 pos)
		{
			setPosition(instance_, entity, pos);
		}

		public System.IntPtr GetPosition(System.IntPtr entity)
		{
			return getPosition(instance_, entity);
		}

		public System.IntPtr GetRotation(System.IntPtr entity)
		{
			return getRotation(instance_, entity);
		}

	}

}
