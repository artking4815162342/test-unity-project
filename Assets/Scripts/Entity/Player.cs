using UnityEngine;

namespace Game.Entity
{
    public interface IPlayer
    {
        float MoveSpeed { get; }

        float RotationSpeed { get; }

        Rigidbody Rigidbody { get; }

        Transform MainTransform { get; }

        Transform BulletLauncherTransform { get; }
    }

    public sealed class Player : LiveEntity, IPlayer
    {
        [Range(0f, 100f)]
        [SerializeField]
        private float _moveSpeed = 1f;

        [Range(0f, 10f)]
        [SerializeField]
        private float _rotationSpeed = 1f;

        [SerializeField]
        private Rigidbody _rigidbody;

        public float MoveSpeed => _moveSpeed;

        public float RotationSpeed => _rotationSpeed;

        public Rigidbody Rigidbody => _rigidbody;

        protected override void InitActions()
        {
            EntityActionController = new PlayerActionController(this);
        }
    }
}