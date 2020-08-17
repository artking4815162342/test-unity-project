using UnityEngine;
using Game.Entity;
using Game.GeneralModule;

namespace Game.MobController
{
    public sealed class MobController : MonoBehaviour
    {
        [SerializeField]
        private SimpleMob _mob;

        private IHealthUI _healthUI;

        private void Start()
        {
            _healthUI = new HealthUIModule(_mob, _mob);
        }
    }
}