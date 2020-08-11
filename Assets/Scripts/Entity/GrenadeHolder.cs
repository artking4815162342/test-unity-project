using System;
using UnityEngine;

namespace Game.Entity
{
    public sealed class GrenadeHolder : BaseSceneEntity, ICanPickup
    {
        [SerializeField]
        private InventoryType _invetoryType;

        [Range(1, 10)]
        [SerializeField]
        private int _pickupCount = 1;

        public InventoryType InventoryType => _invetoryType;

        public int Count => _pickupCount;

        public void PickupEndProcess()
        {
            Destroy(this.gameObject);
        }
    }
}