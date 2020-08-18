using UnityEngine;

namespace Game.FX
{
    public class InstantParticleFX : BaseFX
    {
        [SerializeField]
        private ParticleSystem _particleSystem;

        [SerializeField]
        private ParticleSystemRenderer _particleRenderer;

        private void Awake()
        {
            _particleRenderer.material.color = _color;
        }

        public override void Play()
        {
            _particleSystem.Play();

            //TODO: change to release to gameobject pool
            Invoke("Destroy", _duration);
        }
       
        private void Destroy()
        {
            Destroy(this.gameObject);
        }
    }
}