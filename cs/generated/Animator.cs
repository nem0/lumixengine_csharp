using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	[NativeComponent(Type = "animator")]
	public class Animator : Component
	{
		public Animator(Entity _entity)
			: base(_entity,  getModule(_entity.instance_, "animator" )) { }


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getSource(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setSource(IntPtr module, int cmp, string value);


		public string Source
		{
			get { return getSource(module_, entity_.entity_Id_); }
			set { setSource(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static uint getDefaultSet(IntPtr module, int cmp);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setDefaultSet(IntPtr module, int cmp, uint value);


		public uint DefaultSet
		{
			get { return getDefaultSet(module_, entity_.entity_Id_); }
			set { setDefaultSet(module_, entity_.entity_Id_, value); }
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAnimatorInput(IntPtr instance, int cmp, uint a0, uint a1);

		public void SetAnimatorInput(uint a0, uint a1)
		{
			setAnimatorInput(module_, entity_.entity_Id_, a0, a1);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAnimatorInput(IntPtr instance, int cmp, uint a0, float a1);

		public void SetAnimatorInput(uint a0, float a1)
		{
			setAnimatorInput(module_, entity_.entity_Id_, a0, a1);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAnimatorInput(IntPtr instance, int cmp, uint a0, bool a1);

		public void SetAnimatorInput(uint a0, bool a1)
		{
			setAnimatorInput(module_, entity_.entity_Id_, a0, a1);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getAnimatorInputIndex(IntPtr instance, int cmp, string a0);

		public int GetAnimatorInputIndex(string a0)
		{
			return getAnimatorInputIndex(module_, entity_.entity_Id_, a0);
		}

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void setAnimatorIK(IntPtr instance, int cmp, uint a0, float a1, Vec3 a2);

		public void SetAnimatorIK(uint a0, float a1, Vec3 a2)
		{
			setAnimatorIK(module_, entity_.entity_Id_, a0, a1, a2);
		}

	} // class
} // namespace
