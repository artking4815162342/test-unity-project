using UnityEngine;
using Game.Facade;

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

        public IEntityActionController EntityActionController { get; protected set; }

        protected abstract void InitActions();

        protected virtual void Start()
        {
            InitActions();

            GameInfrastructure.GetInstance.EntityFacade
                .OnChangeExistanceStatus(new Facade.EntityEventArgs(this, true));
        }

        protected virtual void OnDestroy()
        {
            GameInfrastructure.GetInstance.EntityFacade
                .OnChangeExistanceStatus(new Facade.EntityEventArgs(this, false));
        }

        protected virtual void Destroy()
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}