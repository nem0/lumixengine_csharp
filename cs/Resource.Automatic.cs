using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public partial class Resource	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static Resource.State getState(IntPtr instance);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool isEmpty(IntPtr instance);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool isReady(IntPtr instance);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool isFailure(IntPtr instance);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getRefCount(IntPtr instance);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int size(IntPtr instance);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getPath(IntPtr instance);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static System.IntPtr getResourceManager(IntPtr instance);


		public enum State
		{
			EMPTY,
			READY,
			FAILURE,
		}

		public Resource(IntPtr _instance)
		{
			instance_ = _instance;
		}

		public Resource.State GetState()
		{
			return getState(instance_);
		}

		public bool IsEmpty()
		{
			return isEmpty(instance_);
		}

		public bool IsReady()
		{
			return isReady(instance_);
		}

		public bool IsFailure()
		{
			return isFailure(instance_);
		}

		public int GetRefCount()
		{
			return getRefCount(instance_);
		}

		public int Size()
		{
			return size(instance_);
		}

		public string GetPath()
		{
			return getPath(instance_);
		}

		public ResourceManagerBase GetResourceManager()
		{
			return new ResourceManagerBase(getResourceManager(instance_));
		}

		public static implicit operator System.IntPtr(Resource _value)
		{
			 return _value.instance_;
		}
	}

}
