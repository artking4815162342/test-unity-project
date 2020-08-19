using UnityEngine;
using Game.Entity;
using Game.Facade;

namespace Game.GeneralModule
{
    public abstract class BaseBullet : MonoBehaviour
    {
        [SerializeField]
        protected bool _isCollideOwner = false;

        [SerializeField]
        protected Collider _mainCollider;

        protected BaseSceneEntity _owner;
        protected bool _isInited = false;

        public void Init(BaseSceneEntity entity)
        {
            _owner = entity;
            _isInited = true;
        }

        private void OnBecameInvisible()
        {
            enabled = false;
        }

        private void OnBecameVisible()
        {
            enabled = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_isInited == false) {
                Physics.IgnoreCollision(
                   _mainCollider, collision.collider);
            }

            if (CheckIgnore(collision.gameObject)) {
                Physics.IgnoreCollision(
                    _mainCollider, collision.collider);
                return;
            }

            Hit();
            PlayFX();

            GameObject.Destroy(this.gameObject);
        }

        private void OnTriggerEnter(Collider collision)
        {
            if (CheckIgnore(collision.gameObject)) {
                return;
            }

            Hit();
            PlayFX();

            GameObject.Destroy(this.gameObject);
        }

        protected abstract void Hit();

        protected abstract void PlayFX();

        protected virtual bool CheckIgnore(GameObject gameObject)
        {
            if (_isCollideOwner == false) {
                var targetGO = gameObject.transform.root.gameObject;
                var targetEntity = GameInfrastructure
                    .GetInstance.EntityFacade.EntityAggregator.TryGetEntity(targetGO);

                if (targetEntity == _owner) {
                    return true;
                }
            }

            return false;
        }
    }
}