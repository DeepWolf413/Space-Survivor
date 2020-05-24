using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Gameplay
{
    public class WeaponBehaviour : MonoBehaviour
    {
        #region Fields

        /// <summary>
        /// The data of the weapon.
        /// </summary>
        [SerializeField]
        private WeaponData data = null;
        
        /// <summary>
        /// The modules for the <see cref="WeaponBehaviour"/>.
        /// </summary>
        [SerializeField, Tooltip("The modules for the weapon.")]
        private WeaponModule[] modules;

        /// <summary>
        /// When the <see cref="WeaponBehaviour"/> can be used again. This is a timestamp calculated as such: <see cref="Time.time"/> + <see cref="useRate"/>.
        /// </summary>
        private float nextUse = 0.0f;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the data of the weapon.
        /// </summary>
        public WeaponData Data => data;

        public bool IsUsing { get; private set; }

        #endregion

        #region Unity callbacks

        private void OnValidate()
        {
            modules = GetComponents<WeaponModule>();
        }

        private void Update()
        {
            if (IsUsing && !IsOnCooldown())
            { Use(); }
        }

        #endregion

        #region Public methods

        public T GetData<T>() where T : WeaponData => Data as T;

        public void BeginUse()
        {
            if (!CanUse())
            { return; }

            for (int i = 0; i < modules.Length; i++)
            { modules[i].OnBeginUse(); }
            IsUsing = true;
        }

        public void EndUse()
        {
            if (!IsUsing)
            { return; }
            
            CancelInvoke(nameof(Use));
            
            for (int i = 0; i < modules.Length; i++)
            { modules[i].OnEndUse(); }
            IsUsing = false;
        }

        public bool CanUse()
        {
            for (int i = 0; i < modules.Length; i++)
            {
                if (!modules[i].CanUse())
                { return false; }
            }

            return true;
        }

        public bool TryGetModule<TModule>(out TModule module) => TryGetComponent(out module);

        #endregion

        #region Private methods

        private void Use()
        {
            bool endUse = false;
            for (int i = 0; i < modules.Length; i++)
            {
                if (!modules[i].CanUse())
                { endUse = true; }
                modules[i].OnUse();
            }

            ResetUseRate();
            if (endUse)
            { EndUse(); }
        }

        private bool IsOnCooldown() => nextUse > Time.time;

        private void ResetUseRate() => nextUse = Time.time + data.UseRate;

        #endregion
    }
}