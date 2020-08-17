using System;
using UnityEngine;

namespace Game.Entity
{
    public sealed class Player : BaseSceneEntity, IHealth
    {
        [Range(0f, 100f)]
        [SerializeField]
        private float _moveSpeed = 1f;

        [Range(0f, 10f)]
        [SerializeField]
        private float _rotationSpeed = 1f;

        [Range(10, 100)]
        [SerializeField]
        private int _maxHealth;

        [SerializeField]
        private int _attackMouseNum = 0;

        public float MoveSpeed => _moveSpeed;

        public float RotationSpeed => _rotationSpeed;

        public int MaxHealth => _maxHealth;

        public int Health { get; private set; }

        public int AttackMouseNum => _attackMouseNum;

        private void Awake()
        {
            Health = MaxHealth;
        }
    }
}