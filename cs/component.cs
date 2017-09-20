using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace Lumix
{
	public class Component
	{
		private static byte[] s_imgui_text_buffer = new byte[4096];

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		protected extern static int create(IntPtr universe, int entity, string cmp_type);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		protected extern static IntPtr getScene(IntPtr universe, string cmp_type);

		public Entity entity;

		public virtual void onInspector()
		{
			/*var type = this.GetType();
			foreach (var f in type.GetFields())
			{
				if (!f.IsPublic) continue;
				
				var val = f.GetValue(this);
				Type val_type = val.GetType();
				
				if (val_type == typeof(float))
				{
					float f_val = (float)val;
					ImGui.DragFloat(f.Name, ref f_val, 0.1f, float.MaxValue, float.MaxValue, "%f", 1);
				}
				else if (val_type == typeof(bool))
				{
					bool b_val = (bool)val;
					ImGui.Checkbox(f.Name, ref b_val);
				}
				else if (val_type == typeof(int))
				{
					int i_val = (int)val;
					ImGui.InputInt(f.Name, ref i_val, 1, 10, 0);
				}
				else if (val_type == typeof(string))
				{
					string s_val = (string)val;
					byte[] str_bytes = System.Text.Encoding.ASCII.GetBytes(s_val);
					str_bytes.CopyTo(s_imgui_text_buffer, 0);
					if (ImGui.InputText(f.Name, s_imgui_text_buffer, 0, IntPtr.Zero, IntPtr.Zero))
					{
						string new_val = System.Text.Encoding.ASCII.GetString(s_imgui_text_buffer);
						f.SetValue(this, new_val);
					}
				}
				else
				{
					ImGui.Text(f.Name + " = " + val.ToString());
				}
			}*/
		}
	}

	public class NativeComponent : Component
	{

	}
}