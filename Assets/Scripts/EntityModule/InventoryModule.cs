using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Game.InputManagment;

namespace Game.GeneralModule
{
    public delegate void InventoryEvent(InventoryEventArgs e);

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
        private SortedDictionary<InventoryType, InventoryData> _items;

        private InventoryData _selectedItem;

        public IInventoryReadonly InventoryReadonly => this;

        public InventoryModule()
        {
            _changeItem = delegate { };
            _items = new SortedDictionary<InventoryType, InventoryData>();
        }

        public void Add(InventoryType type, int count)
        {
            if (_items.ContainsKey(type)) {
                _items[type].Count += count;
            }
            else {
                var data = new InventoryData(type, count);
                _items.Add(type, data);
                UpdateSelected();
            }

            _changeItem.Invoke(new InventoryEventArgs(type));
        }

        public void Remove(InventoryType type, int count)
        {
            if (_items.ContainsKey(type)) {
                var value = _items[type].Count - 1;

                _items[type].Count = Mathf.Clamp(
                    value, 
                    0, 
                    int.MaxValue);

                if (_items[type].Count == 0) {
                    _items.Remove(type);
                    UpdateSelected();
                }
            }

            _changeItem.Invoke(new InventoryEventArgs(type));
        }

        public bool Contains(InventoryType type, int count)
        {
            if (!_items.ContainsKey(type)) {
                return false;
            }

            var currentCount = _items[type].Count;
            return currentCount >= count;
        }

        public void ProcessPickupData(PickupChangeData data)
        {
            if (data == null || !data.Success || data.type == InventoryType.None) {
                return;
            }

            Add(data.type, data.count);
        }

        public void AddChangeItemEventListener(InventoryEvent @event)
        {
            _changeItem += @event;
        }

        public void RemoveChangeItemEventListener(InventoryEvent @event)
        {
            _changeItem -= @event;
        }

        public IInventoryDataReadonly GetItem(InventoryType type)
        {
            InventoryData result;
            _items.TryGetValue(type, out result);

            return result;
        }

        public IEnumerable<IInventoryDataReadonly> GetAll()
        {
            var readonlyCollection = _items.Values;
            return readonlyCollection;
        }

        private void UpdateSelected()
        {
            if (_items.Count == 0) {
                _selectedItem = null;
                return;
            }

            if (_selectedItem == null || _selectedItem.Count == 0) {
                _selectedItem = _items.First().Value;
                _selectedItem.IsSelected = true;

                _changeItem.Invoke(new InventoryEventArgs(_selectedItem.Type));
                return;
            }
        }

        public void ChangeSelect(PlayerSelectCommand command)
        {
            if (_items.Count < 2) {
                return;
            }

            InventoryData item = null;

            if (command.down) {
                item = _items
                    .Values
                    .Where(e => e.ID > _selectedItem.ID)
                    .FirstOrDefault();
            }
            else if(command.up) {
                item = _items
                    .Values
                    .Where(e => e.ID < _selectedItem.ID)
                    .LastOrDefault();
            }

            if (item != null) {
                _selectedItem.IsSelected = false;
                item.IsSelected = true;

                _selectedItem = item;
                _changeItem.Invoke(new InventoryEventArgs(_selectedItem.Type));
            }
        }
    }
}