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

        private void SendExistanceEvent(bool value)
        {
            var eventArgs = new EntityEventArgs(this, value);

            GameInfrastructure.GetInstance.EntityFacade.EntityEventProvider
                .SendEvent(eventArgs);
        }

        protected virtual void Start()
        {
            InitActions();

            SendExistanceEvent(true);
        }

        protected virtual void OnDestroy()
        {
            SendExistanceEvent(false);
        }

        protected virtual void Destroy()
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}