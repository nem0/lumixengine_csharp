using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public partial class Universe	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getEntityByName(IntPtr instance, string name);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int instantiatePrefab(IntPtr instance, System.IntPtr prefab, Vec3 pos, Quat rot, float scale);


		public Universe(IntPtr _instance)
		{
			instance_ = _instance;
		}

		public Entity GetEntityByName(string name)
		{
			int x = getEntityByName(instance_, name);
			 if(x < 0) return null;
			return new Entity(instance_, x);
		}

		public Entity InstantiatePrefab(PrefabResource prefab, Vec3 pos, Quat rot, float scale)
		{
			int x = instantiatePrefab(instance_, prefab, pos, rot, scale);
			 if(x < 0) return null;
			return new Entity(instance_, x);
		}

		public static implicit operator System.IntPtr(Universe _value)
		{
			 return _value.instance_;
		}
	}

}
