using System;
using System.Collections;
using System.Collections.Generic;
using Game.Entity;
using UnityEngine;

namespace Game.GeneralModule
{
    public delegate void InventoryEvent(InventoryEventArgs e);

    public interface IInventory
    {
        IInventoryReadonly InventoryReadonly { get; }

        void Add(InventoryType type, int count);

        void Remove(InventoryType type, int count);

        bool Contains(InventoryType type, int count);

        void ProcessPickupData(PickupChangeData data);

        IReadOnlyDictionary<InventoryType, int> GetAll();

        void AddChangeItemEventListener(InventoryEvent @event);

        void RemoveChangeItemEventListener(InventoryEvent @event);
    }

    public interface IInventoryReadonly
    {
        bool Contains(InventoryType type, int count);

        IReadOnlyDictionary<InventoryType, int> GetAll();

        void AddChangeItemEventListener(InventoryEvent @event);

        void RemoveChangeItemEventListener(InventoryEvent @event);
    }

    public sealed class InventoryEventArgs : EventArgs
    {
        public InventoryType Type { get; private set; }

        public InventoryEventArgs(InventoryType type)
        {
            Type = type;
        }
    }

    public sealed class InventoryModule : BaseModule, IInventory, IInventoryReadonly
    {
        private event InventoryEvent _changeItem;
        private Dictionary<InventoryType, int> _items;

        public IInventoryReadonly InventoryReadonly => this;

        public InventoryModule()
        {
            _changeItem = delegate { };
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

            _changeItem.Invoke(new InventoryEventArgs(type));
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

            _changeItem.Invoke(new InventoryEventArgs(type));
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

        public IReadOnlyDictionary<InventoryType, int> GetAll()
        {
            return _items;
        }

        public void AddChangeItemEventListener(InventoryEvent @event)
        {
            _changeItem += @event;
        }

        public void RemoveChangeItemEventListener(InventoryEvent @event)
        {
            _changeItem -= @event;
        }
    }
}