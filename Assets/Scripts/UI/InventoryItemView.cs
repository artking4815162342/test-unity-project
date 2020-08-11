using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public sealed class InventoryItemView : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;

        [SerializeField]
        private Text _count;

        [SerializeField]
        private Image _selected;

        public void Init(KeyValuePair<InventoryType, int> item)
        {
            var data = Storage.InventoryItemStorageProxy
                .GetInstance.GetData((int)item.Key);

            if (data == null) {
                Debug.LogError("[InventoryItemView:Init] Invalid data!");
            }

            _icon.color = data.Color;
            _count.text = item.Value.ToString();
            _selected.gameObject.SetActive(false);
        }
    }
}