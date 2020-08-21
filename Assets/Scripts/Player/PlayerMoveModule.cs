using UnityEngine;
using Game.Entity;
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
        private readonly IPlayer _player;
        private readonly Rigidbody _rigidbody;
        private readonly IInventoryReadonly _inventory;

        private bool _isBlockAIM = false;

        public PlayerMoveModule(IPlayer player, IInventoryReadonly inventory)
        {
            _player = player;
            _rigidbody = _player.Rigidbody;
            _inventory = inventory;
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
            if (_inventory.SelectedItem != null && Input.GetButtonDown(InputNames.Fire1)) {
                _isBlockAIM = true;
                return;
            }

            if (Input.GetButtonUp(InputNames.Fire1)) {
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
            Vector3 resultVelocity = localDirection.normalized * _player.MoveSpeed;

            _rigidbody.MovePosition(_rigidbody.position + resultVelocity * Time.fixedDeltaTime);

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