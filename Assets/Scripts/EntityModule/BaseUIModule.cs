using System;
using Game.Entity;
using UnityEngine;
using Game.UI;

namespace Game.GeneralModule
{
    public abstract class BaseUIModule : BaseModule
    {
        protected enum SpawnType
        {
            Canvas,
            Canvas3D,
        }

        protected BaseSceneEntity _entity;

        protected abstract SpawnType Type { get; }

        protected abstract string PrefabPath { get; }

        protected BaseUIElement _instanceUI; 

        public BaseUIModule(BaseSceneEntity entity)
        {
            _entity = entity;
        }

        protected void SpawnSavedProcess()
        {
            try {
                Spawn();
            }
            catch (Exception e) {
                //TODO: change to logger
                Debug.LogException(e);
            }
        }

        private void Spawn()
        {
            Transform parent;

            switch (Type) {
                case SpawnType.Canvas:
                    parent = UI.CanvasContainer.GetInstance.MainTransform;
                    break;
                case SpawnType.Canvas3D:
                    parent = _entity.MainTransform;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(Type.ToString());
            }

            var prefab = Resources.Load<BaseUIElement>(PrefabPath);
            if (prefab == null) {
                throw new NullReferenceException($"Invalid prefab path: {PrefabPath}");
            }

            _instanceUI = GameObject.Instantiate(prefab, parent);
            InitInstance();
        }

        protected abstract void InitInstance();
    }
}
