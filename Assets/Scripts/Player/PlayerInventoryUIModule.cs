using Game.Entity;
using Game.GeneralModule;

namespace Game.PlayerController
{
    public interface IPlayerInventoryUI
    {

    }

    public sealed class PlayerInventoryUIModule : BaseUIModule, IPlayerInventoryUI
    {
        private const string _path = "UI/Modules/InventoryWidget";

        protected override SpawnType Type => SpawnType.Canvas;

        protected override string PrefabPath => _path;

        private readonly IInventoryReadonly _inventory;

        public PlayerInventoryUIModule(BaseSceneEntity entity, IInventoryReadonly inventory) : base(entity)
        {
            _inventory = inventory;

            SpawnSavedProcess();
        }

        protected override void InitInstance()
        {
            _instanceUI.Init(_inventory);
        }
    }
}