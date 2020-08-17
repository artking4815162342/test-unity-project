using System;
using UnityEngine;

namespace Game.Entity
{
    public sealed class Player : LiveEntity
    {
        [Range(0f, 100f)]
        [SerializeField]
        private float _moveSpeed = 1f;

        [Range(0f, 10f)]
        [SerializeField]
        private float _rotationSpeed = 1f;

        [SerializeField]
        private int _attackMouseNum = 0;

        public float MoveSpeed => _moveSpeed;

        public float RotationSpeed => _rotationSpeed;

        public int AttackMouseNum => _attackMouseNum;



        protected override void InitActions()
        {
            EntityActionController = new PlayerActionController(this);
        }


    }
}