using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Storage
{
    public interface IStorage<T> where T: IStorageData
    {
        int Count { get; }

        T GetData(int id);

        IReadOnlyCollection<T> GetAll();
    }

    public abstract class BaseScriptableStorage<T> : ScriptableObject, IStorage<T> where T : IStorageData
    {
        [SerializeField]
        private List<T> _data;

        protected Dictionary<int, T> _dataMap;

        public int Count => _dataMap.Count;

        public void Init()
        {
            _dataMap = new Dictionary<int, T>();
            foreach (var item in _data) {
                if (ValidateData(item)) {
                    _dataMap.Add(item.ID, item);
                }
            }
        }

        public T GetData(int id)
        {
            T data;
            _dataMap.TryGetValue(id, out data);

            return data;
        }

        public IReadOnlyCollection<T> GetAll()
        {
            return _dataMap.Values;
        }

        protected virtual bool ValidateData(T data)
        {
            var validate = _dataMap.ContainsKey(data.ID) == false && data.ID != 0;

            if (validate) {
                return true;
            }
            else {
                //TODO: change to logger
                Debug.LogError("[BaseScriptableStorage::ValidateData] " +
                    $"Invalid data! ID: {data.ID}, Type: {typeof(T)}");
                return false;
            }
        }
    }
}