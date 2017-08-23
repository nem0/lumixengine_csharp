using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

 
namespace Lumix
{ 

 
public class Entity
{
	public int _entity_id;
	public IntPtr _universe;

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
	
	
	public void destroy()
	{
		destroy(_universe, _entity_id);
		_entity_id = -1;
	}
	
	
	public T createComponent<T>() where T : Component, new()
	{
		T cmp = new T();
		cmp.entity = this;
		cmp.create();
		return cmp;
	}
	
	
	public string name
	{
		get { return getName(_universe, _entity_id); }
		set { setName(_universe, _entity_id, value); }
	}
	
	
	public Vec3 position
	{
		get { return getPosition(_universe, _entity_id); }
		set { setPosition(_universe, _entity_id, value); }
	}
	
	
	public Quat rotation
	{
		get { return getRotation(_universe, _entity_id); }
		set { setRotation(_universe, _entity_id, value); }
	}
}


}