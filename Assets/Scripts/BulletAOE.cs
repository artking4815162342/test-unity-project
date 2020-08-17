using System;
using UnityEngine;
using Game.GeneralModule;
using Game.Entity;

namespace Game.GeneralModule
{
    public sealed class BulletAOE : BaseBullet
    {
        [SerializeField]
        private float _radius = 2f;

        [SerializeField]
        private int _count = 500;

        protected override void Hit()
        {
            _owner.EntityActionController
                .GiveDamageAOE(this.transform.position, _radius, _count);
        }

        protected override void PlayFX() { }
    }
}