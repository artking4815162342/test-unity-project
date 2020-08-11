using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Storage
{
    [CreateAssetMenu(fileName = _name, menuName = _path, order = 1)]
    public class InventoryItemData : BaseScriptableData
    {
        private const string _name = "InventoryItemData";
        private const string _path = "ScriptableObjects/ItemData/InventoryItemData";

        [SerializeField]
        private InventoryType _inventoryType;

        [SerializeField]
        private Color _color;

        public InventoryType Type => _inventoryType;

        public Color Color => _color;

        public override int ID => (int)_inventoryType;
    }
}