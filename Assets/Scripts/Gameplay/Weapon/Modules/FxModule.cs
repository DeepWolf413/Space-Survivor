using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class FxModule : WeaponModule
    {
        [SerializeField]
        private ParticleSystem shootSpark = null;
        
        public override void OnBeginUse()
        {
            if (!shootSpark)
            { return; }
            shootSpark.gameObject.SetActive(true);
        }

        public override void OnUse()
        {
            
        }
    }
}