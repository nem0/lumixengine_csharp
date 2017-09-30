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

		public Resource.State CurrentState
		{
			get
			{
				return getState(instance_);
			}
		}

		public bool IsEmpty
		{
			get
			{
				return isEmpty(instance_);
			}
		}

		public bool IsReady
		{
			get
			{
				return isReady(instance_);
			}
		}

		public bool IsFailure
		{
			get
			{
				return isFailure(instance_);
			}
		}

		public int RefCount
		{
			get
			{
				return getRefCount(instance_);
			}
		}

		public int Size
		{
			get
			{
				return size(instance_);
			}
		}

		public string Path
		{
			get
			{
				return getPath(instance_);
			}
		}

		public ResourceManagerBase ResourceManager
		{
			get
			{
				return new ResourceManagerBase(getResourceManager(instance_));
			}
		}

		public static implicit operator System.IntPtr(Resource _value)
		{
			 return _value.instance_;
		}
	}

}
