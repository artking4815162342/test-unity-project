using System;
using UnityEngine;

namespace Game.Entity
{
    public abstract class BaseSceneEntity : MonoBehaviour
    {
        [SerializeField]
        private Transform _mainTransform;

        [SerializeField]
        private Transform _bulletLauncherTransform;

        public Transform MainTransform => _mainTransform;

        public Transform BulletLauncherTransform => _bulletLauncherTransform;

        protected virtual void Start()
        {
            GameInfrastructure.GetInstance.EntityFacade
                .OnChangeExistanceStatus(new Facade.EntityEventArgs(this, true));
        }

        protected virtual void OnDestroy()
        {
            GameInfrastructure.GetInstance.EntityFacade
                .OnChangeExistanceStatus(new Facade.EntityEventArgs(this, false));
        }
    }
}