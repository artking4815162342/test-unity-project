using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Game.GeneralModule;

namespace Game.UI
{
    public sealed class InventoryWidget : BaseUIElement
    {
        [SerializeField]
        private InventoryItemView _template;

        [SerializeField]
        private VerticalLayoutGroup _grid;

        private IInventoryReadonly _inventoryProvider;

        private List<InventoryItemView> _views;

        public override void Init(params object[] @params)
        {
            try
            {
                _inventoryProvider = @params[0] as IInventoryReadonly;
            }
            catch (Exception e)
            {
                ExceptionUI(e);
                return;
            }

            _views = new List<InventoryItemView>();
            UpdateGrid();
            _inventoryProvider.AddChangeItemEventListener(OnChangeInventoryItem);
        }

        private void OnDestroy()
        {
            _inventoryProvider.RemoveChangeItemEventListener(OnChangeInventoryItem);
        }

        private void OnChangeInventoryItem(InventoryEventArgs e)
        {
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            int index = 0;
            var inventoryData = _inventoryProvider.GetAll();

            foreach (var itemData in inventoryData) {
                var starageData = Game.Storage
                    .InventoryItemStorageProxy.GetInstance.GetData(itemData.ID);

                var view = GetViewItem(index);
                view.Init(itemData, starageData);
                view.gameObject.SetActive(true);

                index++;
            }

            while (index < _views.Count) {
                var view = GetViewItem(index);
                view.gameObject.SetActive(false);

                index++;
            }
        }

        private InventoryItemView GetViewItem(int index)
        {
            if (index > _views.Count - 1) {
                CreateViewItem();
            }

            return _views[index];
        }

        private void CreateViewItem()
        {
            InventoryItemView view = Instantiate(
                _template, _grid.transform);
            _views.Add(view);
        }
    }
}