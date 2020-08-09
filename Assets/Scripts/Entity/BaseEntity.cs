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
            GameController.Get.EntityFacade.MobController.Add(this);
        }

        protected virtual void OnDestroy()
        {
            GameController.Get.EntityFacade.MobController.Remove(this);
        }
    }
}