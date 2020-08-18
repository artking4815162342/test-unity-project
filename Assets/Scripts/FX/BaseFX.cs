using System;
using UnityEngine;

namespace Game.FX
{
    public interface IFX
    {
        void Play();
    }

    public abstract class BaseFX : MonoBehaviour, IFX
    {
        [SerializeField]
        protected float _duration;

        [SerializeField]
        protected Color _color;

        public abstract void Play();
    }
}