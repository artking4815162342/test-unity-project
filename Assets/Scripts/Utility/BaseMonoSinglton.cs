using UnityEngine;

public abstract class BaseMonoSinglton<T> : MonoBehaviour where T : BaseMonoSinglton<T>
{
    public static T GetInstance
    {
        get
        {
            return _instance;
        }
    }

    private static T _instance;

    protected virtual void Awake()
    {
        _instance = (T)this;
    }
}