using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.GeneralModule;

namespace Game.PlayerController
{
    public interface IBulletLanuncher
    {
        void Update();

        void FixedUpdate();
    }

    public sealed partial class PlayerBulletLauncher : BaseModule, IBulletLanuncher
    {
        private readonly IInventoryReadonly _inventory;
        private readonly Transform _parent;
        private readonly int _mouseNum;

        //TODO: transfer strings to data
        private readonly LayerMask _mask = LayerMask.GetMask("TargetAIM");
        private readonly string _pointPathPrefab = "PathPoint";
        private readonly string _collisionPointPathPrefab = "CollisionPoint";

        private readonly string _containerPointObjectName = "PathPoints";
        private readonly string _collisionPointObjectName = "CollisionPoint";

        private bool _started = false;
        private List<Transform> _listPoints;
        private Transform _containerPath;
        private Rigidbody _buletBody;
        private Transform _collisionPoint;

        private Vector3 _resultForce;

        private IInventoryDataReadonly _selectedItem;
        private Game.Storage.InventoryItemData _inventoryItemData;

        public PlayerBulletLauncher(IInventoryReadonly inventory, Transform parent, int mouseNum = 0)
        {
            _inventory = inventory;
            _parent = parent;
            _mouseNum = mouseNum;

            CreatePoints();
        }

        private void CreatePoints()
        {
            _containerPath = new GameObject().transform;
            _containerPath.parent = _parent;
            _containerPath.localPosition = Vector3.zero;
            _containerPath.gameObject.SetActive(false);
            _containerPath.name = _containerPointObjectName;

            _listPoints = new List<Transform>();
            var pathPointPrefab = Resources.Load<GameObject>(_pointPathPrefab);

            var collisionPointPrefab = Resources.Load<GameObject>(_collisionPointPathPrefab);
            _collisionPoint = GameObject.Instantiate(collisionPointPrefab).GetComponent<Transform>();
            _collisionPoint.name = _collisionPointObjectName;
            _collisionPoint.gameObject.SetActive(false);

            for (int i = 0; i < _dotsCount; i++) {
                var point = GameObject.Instantiate<GameObject>(pathPointPrefab, _containerPath);
                point.SetActive(true);
                _listPoints.Add(point.transform);
            }
        }

        private void SetPointsState(bool value)
        {
            _containerPath.gameObject.SetActive(value);
            _collisionPoint.gameObject.SetActive(value);
        }

        public void Update()
        {
            if (_started == false && Input.GetMouseButtonDown(_mouseNum)) {
                if (CanStart()) {
                    _started = true;
                    StartProcess();
                }
            }

            if (_started && Input.GetMouseButtonUp(_mouseNum)) {
                _started = false;
                EndProcess();
            }
        }

        public void FixedUpdate()
        {
            if (_started) {
                DrowProcess();
            }
        }

        private bool CanStart()
        {
            _selectedItem = _inventory.SelectedItem;
            if (_selectedItem == null) {
                return false;
            }

            _inventoryItemData = Game.Storage.InventoryItemStorageProxy
                .GetInstance.GetData(_selectedItem.ID);
            if (_inventoryItemData.Prefab == null || !_inventoryItemData.IsBullet) {
                return false;
            }

            return true;
        }

        private void StartProcess()
        {
            CalcTrigonometricConstants();

            SetPointsState(true);
        }

        private void EndProcess()
        {
            SetPointsState(false);

            Launch();

            _inventory.UseItem(_selectedItem);
        }

        private void Launch()
        {
            var prefab = _inventoryItemData.Prefab;
            var go = GameObject.Instantiate<GameObject>(prefab); //TODO: create gameobject pool
            go.transform.position = _parent.position;
            var rb = go.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.AddForce(_resultForce, ForceMode.VelocityChange);
        }
    }
}