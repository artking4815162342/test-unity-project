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
        private IBulletLanuncher _bulletLanuncher;

        private void Start()
        {
            _inventory = new InventoryModule();
            _pickuper = new PickupModule();

            _inventoryUI = new PlayerInventoryUIModule(
                _player, 
                _inventory.InventoryReadonly);

            _mover = new PlayerMoveModule(
                _player,
                _inventory.InventoryReadonly,
                _player.AttackMouseNum);

            _bulletLanuncher = new PlayerBulletLauncher(
                _player,
                _inventory.InventoryReadonly, 
                _player.BulletLauncherTransform, 
                _player.AttackMouseNum);
        }

        private void FixedUpdate()
        {
            _mover.FixedUpdate();
            _bulletLanuncher.FixedUpdate();
        }

        private void Update()
        {
            _bulletLanuncher.Update();
            _mover.Update();

            UpdateSelectInventory();
        }

        private void UpdateSelectInventory()
        {
            var selectCommand = InputManagment
                .PlayerInputHelper.GetSelectCommand();

            if (selectCommand.Has) {
                _inventory.ChangeSelect(selectCommand);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            var changeData = _pickuper.TryPickup(other);
            _inventory.ProcessPickupData(changeData);
        }
    }
}