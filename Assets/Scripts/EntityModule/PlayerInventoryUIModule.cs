using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Entity;

namespace Game.GeneralModule
{
    public interface IPlayerInventoryUI
    {

    }

    public sealed class PlayerInventoryUIModule : BaseUIModule, IPlayerInventoryUI
    {
        private const string _path = "UI/InventoryWidget";

        protected override SpawnType Type => SpawnType.Canvas;

        protected override string PrefabPath => _path;

        private IInventoryReadonly _inventory;

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