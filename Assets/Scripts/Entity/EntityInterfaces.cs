namespace Game.Entity
{
    public interface IHealth
    {
        int MaxHealth { get; }

        int Health { get; }
    }

    public interface ICanPickup
    {
        InventoryType InventoryType { get; }

        int Count { get; }

        void PickupEndProcess();
    }
}