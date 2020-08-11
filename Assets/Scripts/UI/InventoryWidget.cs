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

        private IInventoryReadonly _inventory;

        private List<InventoryItemView> _views;

        public override void Init(params object[] @params)
        {
            try
            {
                _inventory = @params[0] as IInventoryReadonly;
            }
            catch (Exception e)
            {
                ExceptionUI(e);
                return;
            }

            _views = new List<InventoryItemView>();
            UpdateGrid();
            _inventory.AddChangeItemEventListener(OnChangeInventoryItem);
        }

        private void OnDestroy()
        {
            _inventory.RemoveChangeItemEventListener(OnChangeInventoryItem);
        }

        private void OnChangeInventoryItem(InventoryEventArgs e)
        {
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            int index = 0;
            var inventoryData = _inventory.GetAll();

            foreach (var itemData in inventoryData) {
                var view = GetViewItem(index);
                view.Init(itemData);
                view.gameObject.SetActive(true);

                index++;
            }

            while (index < _views.Count - 1) {
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