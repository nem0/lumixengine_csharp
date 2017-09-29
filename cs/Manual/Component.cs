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

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		protected extern static int entityInput(IntPtr editor, IntPtr universe, string label, int entity);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		protected extern static void setCSharpProperty(IntPtr editor, IntPtr universe, int entity, Component cmp, string property, string value);
        public int componentId_;
        public IntPtr scene_;
        public Entity entity_;
		public Entity entity { get { return entity_; } }

        public Universe Universe
        {
            get { return entity.Universe; }
        }

        public Component()
        {
            
        }

        public Component(Entity _entity, int _componentId, IntPtr _scene)
        {
            entity_ = _entity;
            componentId_ = _componentId;
            scene_ = _scene;
        }
        /// <summary>
        /// gets any component which is attache to the underlying entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>T or null if not exists</returns>
        public T GetComponent<T>() where T : Component
        {
            return entity.GetComponent<T>();
        }

        public T CreateComponent<T>() where T : Component
        {
            return entity.CreateComponent<T>();
        }
        public virtual void Create()
        {

        }
        public virtual void OnInspector(IntPtr editor)
		{
			var type = this.GetType();
			foreach (var f in type.GetFields())
			{
				if (!f.IsPublic) continue;
				if(f.Name == "entity_") continue;
				if(f.Name == "scene_") continue;
				
				var val = f.GetValue(this);
				Type val_type = f.FieldType;
				
				if (val_type == typeof(float))
				{
					float f_val = (float)val;
					if(ImGui.DragFloat(f.Name, ref f_val, 0.1f, float.MaxValue, float.MaxValue, "%f", 1))
					{
						setCSharpProperty(editor, entity.instance_, entity.entity_Id_, this, f.Name, f_val.ToString());
					}
				}
				else if (val_type == typeof(bool))
				{
					bool b_val = (bool)val;
					if(ImGui.Checkbox(f.Name, ref b_val))
					{
						setCSharpProperty(editor, entity.instance_, entity.entity_Id_, this, f.Name, b_val.ToString());
					}
				}
				else if (val_type == typeof(int))
				{
					int i_val = (int)val;
					if(ImGui.InputInt(f.Name, ref i_val, 1, 10, 0))
					{
						setCSharpProperty(editor, entity.instance_, entity.entity_Id_, this, f.Name, i_val.ToString());
					}
				}
				else if (val_type == typeof(string))
				{
					string s_val = (string)val;
					byte[] str_bytes = System.Text.Encoding.ASCII.GetBytes(s_val);
					str_bytes.CopyTo(s_imgui_text_buffer, 0);
					if (ImGui.InputText(f.Name, s_imgui_text_buffer, 0, IntPtr.Zero, IntPtr.Zero))
					{
						string new_val = System.Text.Encoding.ASCII.GetString(s_imgui_text_buffer);
						setCSharpProperty(editor, entity.instance_, entity.entity_Id_, this, f.Name, new_val);
					}
				}
				else if(f.FieldType == typeof(Entity))
				{
					int entity_id = val == null ? -1 : ((Entity)val).entity_Id_;
					int new_entity_id = entityInput(editor, entity.instance_, f.Name, entity_id);
					if(new_entity_id != entity_id)
					{
						setCSharpProperty(editor, entity.instance_, entity.entity_Id_, this, f.Name, new_entity_id.ToString());
					}
				}
				else
				{
						
					if (val == null) 
						ImGui.Text(f.Name + " = null");
					else
						ImGui.Text(f.Name + " = " + val.ToString());
				}
			}
		}
	}

    public class NativeComponent : Component
    {
        public NativeComponent(Entity _entity, int _cmpId, IntPtr _scene)
            : base(_entity, _cmpId, _scene) { }

    }
}