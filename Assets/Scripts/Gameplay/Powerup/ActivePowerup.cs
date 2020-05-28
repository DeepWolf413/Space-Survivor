using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay.PowerupSystem
{
    /// <summary>
    /// Represents an <see cref="ActivePowerup"/>.
    /// </summary>
    public struct ActivePowerup
    {
        private float activeTime;

        #region Contructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivePowerup"/> struct.
        /// </summary>
        /// <param name="powerup"></param>
        public ActivePowerup(Powerup powerup)
        {
            Powerup = powerup;
            activeTime = Time.time + Powerup.Duration;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the powerup.
        /// </summary>
        public Powerup Powerup { get; }

        /// <summary>
        /// Gets the time left of the powerup.
        /// </summary>
        public float TimeLeft => activeTime - Time.time;

        #endregion
    }
}