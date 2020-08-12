using UnityEngine;

namespace Game.Entity
{
    public sealed class InventoryHolder : BaseSceneEntity, ICanPickup
    {
        [SerializeField]
        private InventoryType _invetoryType;

        [Range(1, 10)]
        [SerializeField]
        private int _pickupCount = 1;

        [Range(0.1f, 10f)]
        [SerializeField]
        private float _pickupRadius = 1f;

        public InventoryType InventoryType => _invetoryType;

        public int Count => _pickupCount;

        public void PickupEndProcess()
        {
            Destroy(this.gameObject);
        }

        protected override void Start()
        {
            var collider = this.gameObject
                .AddComponent<SphereCollider>();

            collider.radius = _pickupRadius;
            collider.center = Vector3.zero;
            collider.isTrigger = true;

            base.Start();
        }
    }
}