using System;
using UnityEngine;
using Game.Entity;

namespace Game.Facade
{
    public sealed class EntityFacade : IEntityAggregator, IEntityEventProvider
    {
        private EntityEventProvider _eventProvider;
        private EntityAggregator _entityAggregator;

        public EntityFacade()
        {
            _eventProvider = new EntityEventProvider();
            _entityAggregator = new EntityAggregator();

            _eventProvider.Subscribe(_entityAggregator);
        }

        public void OnChangeExistanceStatus(EntityEventArgs e)
        {
            _eventProvider.OnChangeExistanceStatus(e);
        }

        public BaseSceneEntity TryGetEntity(GameObject mobGameObject)
        {
            return _entityAggregator.TryGetEntity(mobGameObject);
        }
    }
}