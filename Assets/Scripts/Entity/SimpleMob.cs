using System;
using UnityEngine;

namespace Game.Entity
{
    public sealed class SimpleMob : LiveEntity
    {
        protected override void InitActions()
        {
            EntityActionController = new MobActionController(this);
        }
    }
}