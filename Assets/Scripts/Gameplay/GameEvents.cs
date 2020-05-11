using System;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public static class GameEvents
    {
        public static event Action<GameObject> EnemyShipSpawned = delegate { };
        
        public static event Action EnemyShipDestroyed = delegate { };

        public static event Action AsteroidsEventSpawned = delegate { };

        public static void SignalEnemyShipSpawned(GameObject ship) => EnemyShipSpawned?.Invoke(ship);
        
        public static void SignalEnemyShipDestroyed() => EnemyShipDestroyed?.Invoke();

        public static void SignalAsteroidsEventSpawned() => AsteroidsEventSpawned?.Invoke();
    }
}