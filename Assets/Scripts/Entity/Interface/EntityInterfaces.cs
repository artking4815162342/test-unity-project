namespace Game.Entity
{
    public interface IHealth
    {
        event System.Action HealthChange;

        int MaxHealth { get; }

        int Health { get; }

        void TakeDamage(int count);
    }

    public interface ICanPickup
    {
        InventoryType InventoryType { get; }

        int Count { get; }

        void PickupEndProcess();
    }
}