using System;
using System.Collections;
using System.Collections.Generic;
using Game.InputManagment;

namespace Game.GeneralModule
{
    public interface IInventory
    {
        IInventoryReadonly InventoryReadonly { get; }

        void Add(InventoryType type, int count);

        void Remove(InventoryType type, int count);

        bool Contains(InventoryType type, int count);

        void ProcessPickupData(PickupChangeData data);

        void ChangeSelect(PlayerSelectCommand command);
    }

    public interface IInventoryReadonly
    {
        bool Contains(InventoryType type, int count);

        IInventoryDataReadonly GetItem(InventoryType type);

        IEnumerable<IInventoryDataReadonly> GetAll();

        void AddChangeItemEventListener(InventoryEvent @event);

        void RemoveChangeItemEventListener(InventoryEvent @event);
    }
}