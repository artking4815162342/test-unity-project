using UnityEngine;
using Game.Entity;
using Game.InputManagment;
using Game.GeneralModule;

namespace Game.PlayerController
{
    public interface IPlayerMover
    {
        void FixedUpdate();
    }

    public sealed class PlayerMoveModule : BaseModule, IPlayerMover
    {
        private readonly Player _player;
        private readonly int _mouseNum;

        public PlayerMoveModule(Player player, int mouseNum = 0)
        {
            _player = player;
            _mouseNum = mouseNum;
        }

        public void FixedUpdate()
        {
            if (Input.GetMouseButton(_mouseNum)) {
                return;
            }

            var moveCommand = PlayerInputHelper.GetMoveCommand();
            if (moveCommand.Has) {
                Move(moveCommand);
            }
        }

        private void Move(PlayerMoveCommand moveCommand)
        {
            Vector3 fromPosition = _player.MainTransform.position;
            Vector3 toPosition = Vector3.zero;
            Vector3 direction = Vector3.zero;

            if (moveCommand.w) {
                direction += _player.MainTransform.forward;
            }

            if (moveCommand.s) {
                direction -= _player.MainTransform.forward;
            }

            if (moveCommand.d) {
                direction += _player.MainTransform.right;
            }

            if (moveCommand.a) {
                direction -= _player.MainTransform.right;
            }

            toPosition = fromPosition + direction.normalized;
            _player.MainTransform.position = Vector3.Lerp(
               fromPosition,
               toPosition,
               Time.deltaTime * _player.MoveSpeed);

            if (direction.Equals(Vector3.zero)) {
                return;
            }

            Quaternion fromRotation = _player.MainTransform.rotation;
            Quaternion toRotation = Quaternion.LookRotation(direction);

            if (!direction.Equals(-_player.MainTransform.forward)) {
                _player.MainTransform.rotation = Quaternion.Lerp(
                fromRotation,
                toRotation,
                Time.deltaTime * _player.RotationSpeed);
            }
        }
    }
}