using DeepWolf.SpaceSurvivor.Enums;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay.PowerupSystem
{
    public abstract class Powerup : ScriptableObject
    {
        [SerializeField]
        protected EPowerupGroup group = EPowerupGroup.Weapon;

        [SerializeField]
        protected Sprite sprite = null;

        [SerializeField]
        protected float duration = 5.0f;

        #region Properties

        /// <summary>
        /// Gets the group of the <see cref="Powerup"/>.
        /// </summary>
        public EPowerupGroup Group => group;

        /// <summary>
        /// Gets the <see cref="Sprite"/> of the <see cref="Powerup"/>.
        /// </summary>
        public Sprite Sprite => sprite;
        
        /// <summary>
        /// Gets the duration of the <see cref="Powerup"/>.
        /// </summary>
        public float Duration => duration;

        #endregion
        
        public abstract void Activate(PowerupsController owner);

        public abstract void Deactivate(PowerupsController owner);
    }
}