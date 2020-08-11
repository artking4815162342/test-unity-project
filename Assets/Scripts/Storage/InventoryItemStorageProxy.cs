using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Storage
{
    public sealed class InventoryItemStorageProxy : BaseSingleton<InventoryItemStorageProxy>, IStorage<InventoryItemData>
    {
        private InventoryItemStorage _storage;

        public InventoryItemStorageProxy()
        {
            _storage = Resources
                .Load<InventoryItemStorage>(InventoryItemStorage.StoragePath);
            _storage.Init();
            Validate();
        }

        public int Count => _storage.Count;

        public IReadOnlyCollection<InventoryItemData> GetAll()
        {
            return _storage.GetAll();
        }

        public InventoryItemData GetData(int id)
        {
            return _storage.GetData(id);
        }

        private void Validate()
        {
            if (_storage == null) {
                //TODO: change to logger
                Debug.LogError("[InventoryItemStorageProxy::Validate] " +
                    "Invalid storage!");
            }
        }
    }
}