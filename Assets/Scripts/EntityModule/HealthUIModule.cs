using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Game.InputManagment;
using Game.Entity;

namespace Game.GeneralModule
{
    public interface IHealthUI
    {

    }

    public sealed class HealthUIModule : BaseUIModule, IHealthUI
    {
        private const string _path = "UI/Modules/Health";

        protected override SpawnType Type => SpawnType.Canvas3D;

        protected override string PrefabPath => _path;

        private readonly IHealth _health;

        public HealthUIModule(BaseSceneEntity entity, IHealth health) : base(entity)
        {
            _health = health;

            SpawnSavedProcess();
        }

        protected override void InitInstance()
        {
            _instanceUI.Init(_health);
        }
    }
}
