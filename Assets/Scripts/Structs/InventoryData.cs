namespace Game.GeneralModule
{
    public interface IInventoryData
    {
        int ID { get; }

        InventoryType Type { get; set; }

        int Count { get; set; }

        bool IsSelected { get; set; }
    }

    public interface IInventoryDataReadonly
    {
        int ID { get; }

        int Count { get; }

        InventoryType Type { get; }

        bool IsSelected { get; }
    }

    public sealed class InventoryData : IInventoryData, IInventoryDataReadonly
    {
        public int Count { get; set; }

        public int ID => (int)Type;

        public InventoryType Type { get; set; }

        public bool IsSelected { get; set; }

        public InventoryData(InventoryType type, int count)
        {
            Count = count;
            Type = type;

            IsSelected = false;
        }
    }
}