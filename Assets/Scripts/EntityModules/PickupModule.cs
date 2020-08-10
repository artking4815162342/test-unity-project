using System;
using UnityEngine;
using Game.Entity;

namespace Game.GeneralModule
{
    public sealed class PickupChangeData
    {
        public InventoryType type;
        public int count;

        public bool Success => count > 0;
    }

    public interface IPickup
    {
        PickupChangeData TryPickup(Collider collider);
    }

    public sealed class PickupModule : BaseModule, IPickup
    {
        public PickupChangeData TryPickup(Collider collider)
        {
            var colliderRoot = collider.transform.root.gameObject;
            var entity = GameInfrastructure.Get
                .EntityFacade.TryGetEntity(colliderRoot);

            if (entity == null || !(entity is ICanPickup pickupObject)) {
                return default;
            }

            PickupChangeData result = new PickupChangeData();
            result.type = pickupObject.InventoryType;
            result.count = pickupObject.Count;

            pickupObject.PickupEndProcess();

            return result;
        }
    }
}