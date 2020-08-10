using System;
using System.Collections;
using System.Collections.Generic;
using Game.Entity;
using UnityEngine;

namespace Game.GeneralModule
{
    public interface IInventory
    {
        void Add(InventoryType type, int count);

        void Remove(InventoryType type, int count);

        bool Contains(InventoryType type, int count);

        void ProcessPickupData(PickupChangeData data);
    }

    public sealed class InventoryModule : BaseModule, IInventory
    {
        private Dictionary<InventoryType, int> _items;

        public InventoryModule()
        {
            _items = new Dictionary<InventoryType, int>();
        }

        public void Add(InventoryType type, int count)
        {
            if (_items.ContainsKey(type)) {
                _items[type] += count;
            }
            else {
                _items.Add(type, count);
            }
        }

        public void Remove(InventoryType type, int count)
        {
            if (_items.ContainsKey(type)) {
                var value = _items[type] - count;

                _items[type] = Mathf.Clamp(
                    value, 
                    0, 
                    int.MaxValue);
            }
        }

        public bool Contains(InventoryType type, int count)
        {
            if (!_items.ContainsKey(type)) {
                return false;
            }

            var currentCount = _items[type];
            return currentCount >= count;
        }

        public void ProcessPickupData(PickupChangeData data)
        {
            if (data == null || !data.Success || data.type == InventoryType.None) {
                return;
            }

            Add(data.type, data.count);
        }
    }
}