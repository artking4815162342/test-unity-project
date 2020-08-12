using UnityEngine;
using UnityEngine.UI;
using Game.GeneralModule;
using Game.Storage;

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

        public void Init(IInventoryDataReadonly inventoryData, InventoryItemData storageData)
        {
            if (storageData == null || inventoryData == null) {
                //TODO: Change to logger
                Debug.LogError("[InventoryItemView:Init] Invalid data!");
                return;
            }

            _icon.color = storageData.Color;
            _count.text = inventoryData.Count.ToString();
            _selected.gameObject.SetActive(inventoryData.IsSelected);
        }
    }
}