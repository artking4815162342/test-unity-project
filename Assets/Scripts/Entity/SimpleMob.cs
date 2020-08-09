using System;
using UnityEngine;

namespace Game.Entity
{
    public sealed class SimpleMob : BaseSceneEntity, IHealth
    {
        [Range(10, 100)]
        [SerializeField]
        private int _maxHealth;

        public int MaxHealth => _maxHealth;

        public int Health { get; private set; }

        private void Awake()
        {
            Health = MaxHealth;
        }
    }
}