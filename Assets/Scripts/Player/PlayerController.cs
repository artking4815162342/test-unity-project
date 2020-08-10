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

        private void Awake()
        {
            _mover = new PlayerMoveModule(_player);
            _inventory = new InventoryModule();
            _pickuper = new PickupModule();
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