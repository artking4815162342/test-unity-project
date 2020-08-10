using UnityEngine;
using Game.Entity;

public sealed class EntityRotator : MonoBehaviour
{
    [SerializeField]
    private BaseSceneEntity _entity;

    [Range(1f, 100f)]
    [SerializeField]
    private float _speed;

    [SerializeField]
    private Vector3 _rotateAngle = new Vector3(0, 1, 0);

    private bool ActiveState;

    private void Start()
    {
        ActiveState = true;
    }

    private void OnBecameVisible()
    {
        ActiveState = true;
    }

    private void OnBecameInvisible()
    {
        ActiveState = false;
    }

    private void FixedUpdate()
    {
        if (ActiveState == false) {
            return;
        }

        _entity.MainTransform.Rotate(_rotateAngle * Time.deltaTime * _speed);
    }
}