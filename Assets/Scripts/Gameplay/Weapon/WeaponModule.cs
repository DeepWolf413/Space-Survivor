using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    [RequireComponent(typeof(WeaponBehaviour))]
    public class WeaponModule : MonoBehaviour
    {
        [SerializeField]
        protected WeaponBehaviour owner = null;
        
        protected virtual void OnValidate()
        {
            if (!owner)
            { owner = GetComponent<WeaponBehaviour>(); }
        }

        /// <summary>
        /// Called by <see cref="WeaponBehaviour"/> when beginning usage.
        /// </summary>
        public virtual void OnBeginUse() {}

        /// <summary>
        /// Called by <see cref="WeaponBehaviour"/> when ending usage.
        /// </summary>
        public virtual void OnEndUse() {}
        
        /// <summary>
        /// Called by <see cref="WeaponBehaviour"/> when it gets used.
        /// </summary>
        public virtual void OnUse() {}

        /// <summary>
        /// Whether the <see cref="WeaponBehaviour"/> should be able to be used.
        /// <para>
        /// If at least 1 module has CanUse be false, then the weapon won't be able to be used.
        /// </para>
        /// </summary>
        public virtual bool CanUse() => true;

        public bool TryGetOwnerData<T>(out T data) where T : WeaponData
        {
            data = owner.GetData<T>();
            bool isValid = data != null;
            if (!isValid)
            { Debug.Log($"Failed to get weapon data as type '{typeof(T).Name}'"); }
            
            return isValid;
        }
    }
}