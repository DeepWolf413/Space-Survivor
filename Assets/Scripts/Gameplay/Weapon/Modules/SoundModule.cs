using DeepWolf.SpaceSurvivor.Managers;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class SoundModule : WeaponModule
    {
        public override void OnUse() => GameManager.SoundManager.PlayGlobalSound(owner.Data.ShootSfx, ESoundType.Sfx);
    }
}