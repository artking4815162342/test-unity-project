using UnityEngine;
using Game.Entity;
using Game.InputManagment;
using Game.GeneralModule;

namespace Game.PlayerController
{
    public interface IPlayerMover
    {
        void Update();

        void FixedUpdate();
    }

    public sealed class PlayerMoveModule : BaseModule, IPlayerMover
    {
        private readonly Player _player;
        private readonly int _mouseNum;
        private readonly IInventoryReadonly _inventory;

        private Rigidbody _rigidbody;
        private bool _isBlockAIM = false;

        public PlayerMoveModule(Player player, IInventoryReadonly inventory, int mouseNum = 0)
        {
            _player = player;
            _inventory = inventory;
            _mouseNum = mouseNum;

            _rigidbody = player.GetComponent<Rigidbody>();
        }

        public void FixedUpdate()
        {
            if (_isBlockAIM) {
                SetEmptyVelocity();
                return;
            }

            Move();
        }

        public void Update()
        {
            CheckAIM();
        }

        private void CheckAIM()
        {
            if (_inventory.SelectedItem != null && Input.GetMouseButtonDown(_mouseNum)) {
                _isBlockAIM = true;
                return;
            }

            if (Input.GetMouseButtonUp(_mouseNum)) {
                _isBlockAIM = false;
                return;
            }
        }

        private void SetEmptyVelocity()
        {
            _rigidbody.velocity = Vector3.zero;
        }

        private void Move()
        {
            float moveHorizontal = Input.GetAxis(InputNames.Horizontal);
            float moveVertical = Input.GetAxis(InputNames.Vertical);

            Vector3 direction = new Vector3(moveHorizontal, 0f, moveVertical);
            Vector3 localDirection = _player.MainTransform.rotation * direction;

            _rigidbody.velocity = localDirection.normalized * _player.MoveSpeed;

            if (Mathf.Abs(direction.x) < float.Epsilon && Mathf.Abs(direction.y) < float.Epsilon) {
                return;
            }

            Quaternion toRotation = Quaternion.LookRotation(localDirection);
            _rigidbody.rotation = Quaternion.Lerp(
                _rigidbody.rotation,
                toRotation,
                Time.deltaTime * _player.RotationSpeed);
        }
    }
}