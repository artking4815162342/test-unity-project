using System;
using UnityEngine;

namespace Game.Storage
{
    public interface IStorageData
    {
        int ID { get; }
    }

    public abstract class BaseScriptableData : ScriptableObject, IStorageData
    {
        public abstract int ID { get; }
    }
}
