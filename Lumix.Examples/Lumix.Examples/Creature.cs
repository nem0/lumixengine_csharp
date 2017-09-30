using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lumix;

namespace Lumix
{
   
    /// <summary>
    /// Every game component has to inherit <see cref="Component"/>
    /// </summary>
    /// <seealso cref="Component" />
    public class Creature : Component
    {
        int speedInputIdx_ = -1;
        int attackInputIdx = -1;
        int deadInpuIdx = -1;
        AnimController animController_;
        NavmeshAgent agent_;
        float nextFollow;
        /// <summary>
        /// Try to follow the target every N ticks
        /// </summary>
        public float FollowTick = 5.0f;
        /// <summary>
        /// Target to follow
        /// </summary>
        public Entity Target;
        /// <summary>
        /// Speed to follow the target
        /// </summary>
        public float FollowSpeed = 5.0f;
        /// <summary>
        /// Init gets called by lumix right after construnction of this component
        /// </summary>
        void OnStartGame()
        {
            animController_ = GetComponent<AnimController>();
            agent_ = GetComponent<NavmeshAgent>();
            speedInputIdx_ = animController_.GetControllerInputIndex("speed");
            attackInputIdx = animController_.GetControllerInputIndex("attack");
            deadInpuIdx = animController_.GetControllerInputIndex("dead");

        }

        /// <summary>
        /// This function gets called every frame to update your component logic
        /// </summary>you 
        /// <param name="_deltaTime">The delta time.</param>
        void Update(float _deltaTime)
        {
			Target = entity.Universe.GetEntityByName("target");
            ///get agent speed from navigation and set it as input to animation controller
            /// so it can play the right animation
            var agentSpeed = agent_.Speed;
            animController_.SetControllerInput(speedInputIdx_, agentSpeed);

            //try to move to where player is every few seconds
            nextFollow = nextFollow - _deltaTime;
            if (nextFollow > 0) return;

            nextFollow = FollowTick;

            if (Target != null)
            {
                ///get the target pos
                var pos = Target.Position;
                // and move to the postion
                agent_.Navigate(pos, FollowSpeed, 0.1f);
            }
        }

        void Kill()
        {
            //play dead animation
            animController_.SetControllerInput(deadInpuIdx, true);
            //stio following player
            StopFollowing();
            //todo despawn
        }

        void StopFollowing()
        {

            agent_.CancelNavigation();
        }
    }
}
