using UnityEngine;

namespace _ActionFarm.Scripts.Hero
{
    public class HeroWeaponColliderController : MonoBehaviour
    {
        [SerializeField] private Collider WaeponCollider;

        public void EnableWeaponCollider()
        {
            WaeponCollider.enabled = true;
        }

        public void DisableWeaponCollider()
        {
            WaeponCollider.enabled = false;
        }
    }
}