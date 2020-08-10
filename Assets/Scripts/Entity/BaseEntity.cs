using System;
using UnityEngine;

namespace Game.Entity
{
    public abstract class BaseSceneEntity : MonoBehaviour
    {
        [SerializeField]
        private Transform _mainTransform;

        public Transform MainTransform => _mainTransform;

        protected virtual void Start()
        {
            GameInfrastructure.Get.EntityFacade
                .OnChangeExistanceStatus(new Facade.EntityEventArgs(this, true));
        }

        protected virtual void OnDestroy()
        {
            GameInfrastructure.Get.EntityFacade
                .OnChangeExistanceStatus(new Facade.EntityEventArgs(this, false));
        }
    }
}