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

        [SerializeField]
        private GameObject _prefab;

        [SerializeField]
        private bool _isBullet;

        public InventoryType Type => _inventoryType;

        public Color Color => _color;

        public override int ID => (int)_inventoryType;

        public GameObject Prefab => _prefab;

        public bool IsBullet => _isBullet;
    }
}