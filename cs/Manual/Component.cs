using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.ComponentModel;


namespace Lumix
{

	public class Component
	{
        private static byte[] s_imgui_text_buffer = new byte[4096];

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		protected extern static int create(IntPtr world, int entity, string cmp_type);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		protected extern static IntPtr getModule(IntPtr world, string cmp_type);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		protected extern static int entityInput(IntPtr editor, IntPtr world, string label, int entity);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		protected extern static IntPtr resourceInput(IntPtr editor, string label, string type, IntPtr resource);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		protected extern static void pushUndoCommand(IntPtr editor, IntPtr world, int entity, Component cmp, string property, string old_value, string value);

		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		protected extern static int getEntityFromEntityMap(IntPtr entity_map, int entity_id);

		public string Serialize()
		{
			var type = this.GetType();
			var string_builder = new System.Text.StringBuilder();
			string_builder.Append("0|"); // header (version)
			foreach (var f in type.GetFields())
			{
				if (!f.IsPublic) continue;
				if(f.Name == "entity_") continue;
				if(f.Name == "componentId_") continue;
				if(f.Name == "module_") continue;
				
				var val = f.GetValue(this);
				Type val_type = f.FieldType;
				string_builder.Append(val_type.Name);
				string_builder.Append("|");
				string_builder.Append(f.Name);
				string_builder.Append("|");
				
				if(f.FieldType == typeof(Entity))
				{
					string_builder.Append(val == null ? -1 : ((Entity)val).entity_Id_);
				}
				else if(f.FieldType.BaseType == typeof(Resource))
				{
					IntPtr native_res = val == null ? IntPtr.Zero : ((Resource)val).__Instance;
					string path = Resource.getPath(native_res);
					string_builder.Append(path);
				}
				else
				{
					string_builder.Append(val);
				}
				string_builder.Append("|");
			}
			return string_builder.ToString();
		}

		public void Deserialize(string data, IntPtr entity_map)
		{
			var this_type = this.GetType();
			string[] values = data.Split('|');
			int version = int.Parse(values[0]);
			if (version > 0) return; 
			for (int i = 1; i < values.Length - 1; i += 3)
			{
				string type = values[i];
				string name = values[i+1];
				string value = values[i+2];

				var field = this_type.GetField(name);
				Type field_type = field.FieldType;
				if (field_type.Name != type) continue;

				if (field_type == typeof(Entity))
				{
					int entity_id = int.Parse(value);
					entity_id = getEntityFromEntityMap(entity_map, entity_id);
					Entity e = World.GetEntity(entity_id);
					field.SetValue(this, e);
				}
				else if(field_type.BaseType == typeof(Resource))
				{
					var resource = Activator.CreateInstance(field_type, new object[] { value });
					field.SetValue(this, resource);
				}
				else
				{
					var converter = TypeDescriptor.GetConverter(field_type);
					field.SetValue(this, converter.ConvertFrom(value));
				}
			}
		}

		public void OnUndo(IntPtr editor, string property, string value)
		{
			var field = this.GetType().GetField(property);
			if (field == null) return;

			Type field_type = field.FieldType;
			if(field_type == typeof(int))
			{
				field.SetValue(this, int.Parse(value));
			}
			else if(field_type == typeof(float))
			{
				field.SetValue(this, float.Parse(value));
			}
			else if(field_type == typeof(bool))
			{
				field.SetValue(this, bool.Parse(value));
			}
			else if(field_type == typeof(string))
			{
				field.SetValue(this, value);
			}
			else if(field_type == typeof(Lumix.Entity))
			{
				int entity_id = int.Parse(value);
				Entity e = World.GetEntity(entity_id);
				field.SetValue(this, e);
			}
			else if(field_type.BaseType == typeof(Lumix.Resource))
			{
				var resource = Activator.CreateInstance(field_type, new object[] { value });
				field.SetValue(this, resource);
			}
			else
			{
				string msg = "Unsupported type " + field_type.FullName + " = " + value;
				Engine.logError(msg);
				throw new Exception(msg);
			}
		}

        public IntPtr module_;
		protected Entity entity_;
		public Entity entity 
		{ 
			get { return entity_; }
			set 
			{ 
				if(entity_ != null) throw new Exception("Component's entity can not be reset");
				entity_ = value;
				entity_.components_.Add(this);
			}
		}

        public World World
        {
            get { return entity.World; }
        }

        public Component()
        {
            
        }

        public Component(Entity _entity, IntPtr _module)
        {
            entity_ = _entity;
            module_ = _module;
        }

        public T GetComponent<T>() where T : Component
        {
            return entity.GetComponent<T>();
        }

        public T CreateComponent<T>() where T : Component
        {
            return entity.CreateComponent<T>();
        }

        public virtual void OnInspector(IntPtr editor)
		{
			var type = this.GetType();
			foreach (var f in type.GetFields())
			{
				if (!f.IsPublic) continue;
				if(f.Name == "entity_") continue;
				if(f.Name == "componentId_") continue;
				if(f.Name == "module_") continue;
				
				var val = f.GetValue(this);
				Type val_type = f.FieldType;
				
				if (val_type == typeof(float))
				{
					float old_value = (float)val;
					float new_value = old_value;
					if(ImGui.DragFloat(f.Name, ref new_value, 0.1f, float.MaxValue, float.MaxValue, "%f", 0))
					{
						pushUndoCommand(editor, entity.instance_, entity.entity_Id_, this, f.Name, old_value.ToString(), new_value.ToString());
					}
				}
				else if (val_type == typeof(bool))
				{
					bool old_value = (bool)val;
					bool new_value = old_value;
					if(ImGui.Checkbox(f.Name, ref new_value))
					{
						pushUndoCommand(editor, entity.instance_, entity.entity_Id_, this, f.Name, old_value.ToString(), new_value.ToString());
					}
				}
				else if (val_type == typeof(int))
				{
					int old_value = (int)val;
					int new_value = old_value;
					if(ImGui.InputInt(f.Name, ref new_value, 1, 10, 0))
					{
						pushUndoCommand(editor, entity.instance_, entity.entity_Id_, this, f.Name, old_value.ToString(), new_value.ToString());
					}
				}
				else if (val_type == typeof(string))
				{
					string old_value = (string)val;
					byte[] str_bytes = System.Text.Encoding.ASCII.GetBytes(old_value);
					str_bytes.CopyTo(s_imgui_text_buffer, 0);
					s_imgui_text_buffer[str_bytes.Length] = 0;
					if (ImGui.InputText(f.Name, s_imgui_text_buffer, 0, IntPtr.Zero, IntPtr.Zero))
					{
						string new_value = System.Text.Encoding.ASCII.GetString(s_imgui_text_buffer);
						pushUndoCommand(editor, entity.instance_, entity.entity_Id_, this, f.Name, old_value, new_value);
					}
				}
				else if(f.FieldType == typeof(Entity))
				{
					int old_entity_id = val == null ? -1 : ((Entity)val).entity_Id_;
					int new_entity_id = entityInput(editor, entity.instance_, f.Name, old_entity_id);
					if(new_entity_id != old_entity_id)
					{
						pushUndoCommand(editor, entity.instance_, entity.entity_Id_, this, f.Name, old_entity_id.ToString(), new_entity_id.ToString());
					}
				}
				else if(f.FieldType.BaseType == typeof(Resource))
				{
					IntPtr old_res = val == null ? IntPtr.Zero : ((Resource)val).__Instance;
					IntPtr new_res = resourceInput(editor, f.Name, "prefab", old_res);
					if (new_res != old_res)
					{
						string old_path = old_res == IntPtr.Zero ? "" : Resource.getPath(old_res);
						if (new_res == IntPtr.Zero)
						{
							pushUndoCommand(editor, entity.instance_, entity.entity_Id_, this, f.Name, old_path, "");
						}
						else
						{
							pushUndoCommand(editor, entity.instance_, entity.entity_Id_, this, f.Name, old_path, Resource.getPath(new_res));
						}
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
}