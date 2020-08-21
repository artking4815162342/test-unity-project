using UnityEngine;
using System;

namespace Game.Entity
{
    public abstract class LiveEntity : BaseSceneEntity, IHealth
    {
        [Range(100, 2000)]
        [SerializeField]
        private int _maxHealth;

        public event Action HealthChange = delegate { };

        public int MaxHealth => _maxHealth;

        public int Health { get; private set; }

        public void TakeDamage(int count)
        {
            Health = Mathf.Clamp(Health - count, 0, int.MaxValue);
            if (Health == 0) {
                Dead();
            }

            HealthChange.Invoke();
        }

        protected virtual void Awake()
        {
            Health = MaxHealth;
        }

        protected void Dead()
        {
            Destroy();
        }
    }
}