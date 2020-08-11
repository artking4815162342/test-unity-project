using UnityEngine;

namespace Game.Storage
{
    [CreateAssetMenu(fileName = _storageName, menuName = _path, order = 1)]
    public sealed class InventoryItemStorage : BaseScriptableStorage<InventoryItemData>
    {
        private const string _storageName = "InventoryItemStorage";
        private const string _path = "ScriptableObjects/Storage/InventoryItemStorage";

        public static readonly string StoragePath;

        static InventoryItemStorage()
        {
            StoragePath = _path;
        }
    }
}