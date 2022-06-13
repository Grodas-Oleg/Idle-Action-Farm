using _ActionFarm.Scripts.Handler;
using UnityEngine;
using UnityEngine.VFX;

namespace _ActionFarm.Scripts.Hero
{
    public class HeroWeaponView : MonoBehaviour
    {
        [SerializeField] private Collider WaeponCollider;
        [SerializeField] private GameObject _weaponHolder;
        [SerializeField] private VisualEffect _slashVFX;

        public void SetWeaponView(bool status)
        {
            _weaponHolder.SetActive(status);
        }

        public void EnableWeaponCollider()
        {
            WaeponCollider.enabled = true;
            AudioHelper.PlaySoundByName("slash");
            _slashVFX.Play();
        }

        public void DisableWeaponCollider()
        {
            WaeponCollider.enabled = false;
            _slashVFX.Stop();
        }
    }
}