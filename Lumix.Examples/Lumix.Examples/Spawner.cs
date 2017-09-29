using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumix
{
    public class Spawner : Component
    {
        public struct ET
        {
            public Entity Creature;
            public float Time;

            public void Set(float time)
            {
                Time = time;
            }
        }
        List<Entity> spawnedTable_;
        List<ET> despawnTable;
        float nextSpawn_;
        public float SpawnTick = 5;
        public int MaxSpawns = 10;
        public Vec4 SpawnArea = new Vec4(0, 0, 2000, 2000);
        [EditorInfo(Description = "Object which this component should spawn!")]
        public PrefabResource SpawnPrefab;
        /// <summary>
        /// Gets a random location inside SpawnAraea
        /// </summary>
        /// <value>
        /// The random location.
        /// </value>
        public Vec3 RandomLocation
        {
            get
            {
                float x = Mathf.RangeRandom(SpawnArea.x, SpawnArea.z);
                float z = Mathf.RangeRandom(SpawnArea.y, SpawnArea.w);
                int y = 0;
                //todo
                //int y = GetHeightAt(x,z);
                return new Vec3(x, y, z);
            }
        }
        void OnStartGame()
        {
            spawnedTable_ = new List<Entity>();
            despawnTable = new List<ET>();

            nextSpawn_ = SpawnTick;
            if(SpawnPrefab == null)
            {
                //provide default for now
                SpawnPrefab = Resources.Load<PrefabResource>("prefabs/tutorial/creature.fab");
            }
        }

        void Update(float _deltaTime)
        {
            nextSpawn_ -= _deltaTime;
            if (nextSpawn_ > 0)
                return;//nothing todo!

            if (spawnedTable_.Count == MaxSpawns)
                return;//only spawn as much as we need

            //get a random position for the newsy spawned creature
            var pos = RandomLocation;
            //instantiate the prefab as a new entity
            var creature = Universe.InstantiatePrefab(SpawnPrefab, pos, Quat.Identity, 1.0f);
            //remember it for later despawn
            spawnedTable_.Add(creature);
        }

        void ProcessDespawn(float _deltaTime)
        {
            for(int k =0; k < despawnTable.Count;k++)
            {
                despawnTable[k].Set(despawnTable[k].Time - _deltaTime);
                if (despawnTable[k].Time < 0.0f)
                {
                    despawnTable[k].Creature.Destroy();
                    despawnTable.RemoveAt(k);
                    break;//despawn max 1 per frame
                }
            }
        }

        public void Despawn(Entity _creature, float _time = 3.0f)
        {
            for (int k = 0; k < spawnedTable_.Count;k++)
            {
                if(spawnedTable_[k] == _creature)
                {
                    spawnedTable_.Remove(_creature);
                    despawnTable.Add(new ET() { Creature = _creature, Time = _time });
                }
            }
        }
    }
}
