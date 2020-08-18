using UnityEngine;
using Game.FX;

namespace Game.GeneralModule
{
    public sealed class BulletAOE : BaseBullet
    {
        [SerializeField]
        private float _radius = 2f;

        [SerializeField]
        private int _count = 500;

        [SerializeField]
        private BaseFX _effectPrefab;

        protected override void Hit()
        {
            _owner.EntityActionController
                .GiveDamageAOE(this.transform.position, _radius, _count);
        }

        protected override void PlayFX()
        {
            var fx = GameObject.Instantiate(_effectPrefab);

            fx.transform.position = this.transform.position;
            fx.Play();
        }
    }
}