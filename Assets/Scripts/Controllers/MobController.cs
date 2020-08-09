using System;
using System.Collections;
using System.Collections.Generic;
using Game.Entity;
using UnityEngine;

public interface IMobController
{
    void Add(BaseSceneEntity mob);

    void Remove(BaseSceneEntity mob);

    BaseSceneEntity TryGetMob(GameObject mobGameObject);
}

public sealed class MobController : IMobController
{
    private Dictionary<GameObject, BaseSceneEntity> _mobs;

    public MobController()
    {
        _mobs = new Dictionary<GameObject, BaseSceneEntity>();
    }

    public void Add(BaseSceneEntity mob)
    {
        _mobs.Add(mob.gameObject, mob);
    }

    public void Remove(BaseSceneEntity mob)
    {
        _mobs.Remove(mob.gameObject);
    }

    public BaseSceneEntity TryGetMob(GameObject mobGameObject)
    {
        _mobs.TryGetValue(mobGameObject, out var mob);
        return mob;
    }
}