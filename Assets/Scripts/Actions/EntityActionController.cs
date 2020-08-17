using UnityEngine;
using Game.Facade;
using System.Collections.Generic;

namespace Game.Entity
{
    public interface IEntityActionController
    {
        BaseSceneEntity Owner { get; }

        void GiveDamageAOE(Vector3 position, float radius, int count);

        void TakeDamage(int count);
    }

    public abstract class EntityActionController : IEntityActionController
    {
        public BaseSceneEntity Owner { get; private set; }

        private HashSet<BaseSceneEntity> _targets;

        public EntityActionController(BaseSceneEntity owner)
        {
            Owner = owner;

            _targets = new HashSet<BaseSceneEntity>();
        }

        /// <summary>
        /// Give damage out.
        /// </summary>
        public void GiveDamageAOE(Vector3 position, float radius, int count)
        {
            var mask = LayerMask.GetMask(LayerNames.EntityName);
            var targets = Physics.OverlapSphere(position, radius, mask);

            _targets.Clear();

            for (int i = 0; i < targets.Length; i++) {
                var targetGO = targets[i].transform.root.gameObject;
                var targetEntity = GameInfrastructure
                    .GetInstance.EntityFacade.TryGetEntity(targetGO);

                if (targetEntity == null || targetEntity == Owner) {
                    continue;
                }

                _targets.Add(targetEntity);
            }

            foreach (var targetEntity in _targets) {
                if (targetEntity.EntityActionController == null) {
                    //TODO: change to logger
                    Debug.LogError("Empty EntityActionController!");
                    continue;
                }
                targetEntity.EntityActionController.TakeDamage(count);
            }
        }

        /// <summary>
        /// Take damage in.
        /// </summary>
        public void TakeDamage(int count)
        {
            if (Owner is IHealth health) {
                health.TakeDamage(count);
            }
        }
    }
}