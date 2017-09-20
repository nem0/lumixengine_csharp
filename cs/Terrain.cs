using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;


namespace Lumix
{


	public class Terrain : NativeComponent
	{
		private int component_id;
		private IntPtr scene;

		public static string GetCmpType() { return "terrain"; }

		public Terrain(Entity _entity, int _component_id)
		{
			entity = _entity;
			component_id = _component_id;
			scene = getScene(entity._universe, "terrain");
		}

		public Terrain(Entity entity)
		{
			component_id = create(entity._universe, entity._entity_id, "terrain");
			if (component_id < 0) throw new Exception("Failed to create component");
			scene = getScene(entity._universe, "terrain");
			Grass._object = this;
		}


		/* YScale */
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static void setYScale(IntPtr scene, int cmp, float source);
		
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static float getYScale(IntPtr scene, int cmp);
		
		public float YScale
		{
			get{ return getYScale(scene, component_id); }
			set{ setYScale(scene, component_id, value); }
		}


		/* XZScale */
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static void setXZScale(IntPtr scene, int cmp, float source);
		
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static float getXZScale(IntPtr scene, int cmp);
		
		public float XZScale
		{
			get{ return getXZScale(scene, component_id); }
			set{ setXZScale(scene, component_id, value); }
		}


		/* MaterialPath */
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static void setMaterialPath(IntPtr scene, int cmp, string source);
		
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static string getMaterialPath(IntPtr scene, int cmp);
		
		public string MaterialPath
		{
			get{ return getMaterialPath(scene, component_id); }
			set{ setMaterialPath(scene, component_id, value); }
		}


		/* Grass Density */
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static void setGrassDensity(IntPtr scene, int cmp, int index, int source);
		
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static int getGrassDensity(IntPtr scene, int cmp, int index);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static int getGrassCount(IntPtr scene, int cmp);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static void addGrass(IntPtr scene, int cmp);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		private extern static void removeGrass(IntPtr scene, int cmp, int index);


		public class GrassContainer
		{
			public int Length { get { return Terrain.getGrassCount(_object.scene, _object.component_id); } }

			public GrassType this[int index]
			{
				get {
					return new GrassType(_object, index);
				}
			}

			public void add()
			{
				Terrain.addGrass(_object.scene, _object.component_id);
			}

			public void remove(int index)
			{
				Terrain.removeGrass(_object.scene, _object.component_id, index);
			}

			public Terrain _object;
		}


		public GrassContainer Grass = new GrassContainer();


		public class GrassType
		{
			private int _index;
			private Terrain _object;

			public GrassType(Terrain obj, int index)
			{
				_index = index;
				_object = obj;
			}

			public int Density
			{
				get{ return Terrain.getGrassDensity(_object.scene, _object.component_id, _index); }
				set{ Terrain.setGrassDensity(_object.scene, _object.component_id, value, _index); }
			}


		}

	}

}
