using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

 
namespace Lumix
{ 

 
public partial class Entity
{
	public int _entity_id;
	public IntPtr _universe;
	private List<Component> components = new List<Component>();

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	private extern static void setPosition(IntPtr universe, int entity, Vec3 pos);

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	private extern static Vec3 getPosition(IntPtr universe, int entity);

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	private extern static void setRotation(IntPtr universe, int entity, Quat rot);

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	private extern static Quat getRotation(IntPtr universe, int entity);

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	private extern static string getName(IntPtr universe, int entity);

	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	private extern static void setName(IntPtr universe, int entity, string name);
	
	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	private extern static void destroy(IntPtr universe, int entity);
	
	[MethodImplAttribute(MethodImplOptions.InternalCall)]
	private extern static int getComponent(IntPtr universe, int entity, string cmp_type);
	
	public void Destroy()
	{
		destroy(_universe, _entity_id);
		_entity_id = -1;
	}
	
	
	public T GetComponent<T>() where T : Component
	{
		for (int i = 0, c = components.Count; i < c; ++i)
		{
			var cmp = components[i];
			if (cmp is T) return cmp as T;
		}
		
		if(typeof(T).IsSubclassOf(typeof(NativeComponent)))
		{
			var prop = typeof(T).GetProperty("GetCmpType", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
			string cmp_type = (string)prop.GetValue(null, null);

			int cmp_id = getComponent(_universe, _entity_id, cmp_type);
			if (cmp_id < 0) return null;
			
			var cmp = (T)Activator.CreateInstance(typeof(T), this, cmp_id);
			components.Add(cmp);
			return cmp;
		}
		
		return null;
	}
	
	
	public T CreateComponent<T>() where T : Component
	{
		T cmp = (T)Activator.CreateInstance(typeof(T), this);
		components.Add(cmp);
		return cmp;
	}
	
	
	public string Name
	{
		get { return getName(_universe, _entity_id); }
		set { setName(_universe, _entity_id, value); }
	}
	
	
	public Vec3 Position
	{
		get { return getPosition(_universe, _entity_id); }
		set { setPosition(_universe, _entity_id, value); }
	}
	
	
	public Quat Rotation
	{
		get { return getRotation(_universe, _entity_id); }
		set { setRotation(_universe, _entity_id, value); }
	}
}


}