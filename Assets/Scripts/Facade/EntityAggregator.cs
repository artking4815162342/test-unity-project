using System;
using System.Collections;
using System.Collections.Generic;
using Game.Entity;
using UnityEngine;

namespace Game.Facade
{
    public interface IEntityAggregator
    {
        BaseSceneEntity TryGetEntity(GameObject mobGameObject);
    }

    public sealed class EntityAggregator : IEntityAggregator, IObserver<EntityEventArgs>
    {
        private Dictionary<GameObject, BaseSceneEntity> _items;

        public EntityAggregator()
        {
            _items = new Dictionary<GameObject, BaseSceneEntity>();
        }

        public BaseSceneEntity TryGetEntity(GameObject mobGameObject)
        {
            _items.TryGetValue(mobGameObject, out var entity);
            return entity;
        }

        public void Add(BaseSceneEntity entity)
        {
            _items.Add(entity.gameObject, entity);
        }

        public void Remove(BaseSceneEntity entity)
        {
            _items.Remove(entity.gameObject);
        }

        public void OnNext(EntityEventArgs value)
        {
            if (value.Is) {
                Add(value.Entity);
            }
            else {
                Remove(value.Entity);
            }
        }

        public void OnCompleted() { }

        public void OnError(Exception error)
        {
            //TODO: change to logger
            Debug.LogError(error);
        }
    }
}