using System;
using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public static class GameEvents
    {
        public static event Action<GameObject> EnemyShipSpawned = delegate { };
        
        public static event Action EnemyShipDestroyed = delegate { };

        public static event Action PlayerShipDestroyed = delegate { };

        public static event Action AsteroidsEventSpawned = delegate { };

        public static event Action<GameObject> AsteroidSpawned = delegate { };
        
        public static event Action<Pickup> PickupSpawned = delegate { };
        
        public static event Action<Pickup> PickupDespawned = delegate { };

        public static void SignalEnemyShipSpawned(GameObject ship) => EnemyShipSpawned?.Invoke(ship);
        
        public static void SignalEnemyShipDestroyed() => EnemyShipDestroyed?.Invoke();

        public static void SignalPlayerShipDestroyed() => PlayerShipDestroyed?.Invoke();
        
        public static void SignalAsteroidsEventSpawned() => AsteroidsEventSpawned?.Invoke();

        public static void SignalAsteroidSpawned(GameObject asteroid) => AsteroidSpawned?.Invoke(asteroid);
        
        public static void SignalPickupSpawned(Pickup pickup) => PickupSpawned?.Invoke(pickup);
        
        public static void SignalPickupDespawned(Pickup pickup) => PickupDespawned?.Invoke(pickup);
    }
}