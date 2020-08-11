using UnityEngine;
using Game.Entity;
using Game.GeneralModule;

namespace Game.PlayerController
{
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private Player _player;

        private IPlayerMover _mover;
        private IInventory _inventory;
        private IPickup _pickuper;
        private IPlayerInventoryUI _inventoryUI;

        private void Start()
        {
            _mover = new PlayerMoveModule(_player);
            _inventory = new InventoryModule();
            _pickuper = new PickupModule();
            _inventoryUI = new PlayerInventoryUIModule(_player, _inventory.InventoryReadonly);
        }

        private void FixedUpdate()
        {
            _mover.Update();
        }

        private void OnTriggerEnter(Collider other)
        {
            var changeData = _pickuper.TryPickup(other);
            _inventory.ProcessPickupData(changeData);
        }
    }
}