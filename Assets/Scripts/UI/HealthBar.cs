using System;
using UnityEngine;
using UnityEngine.UI;
using Game.GeneralModule;
using Game.Entity;

namespace Game.UI
{
    public sealed class HealthBar : BaseUIElement
    {
        [SerializeField]
        private Image _bar;

        [SerializeField]
        private Text _percent;

        [SerializeField]
        private Canvas _canvas;

        private IHealth _healthProvider;
        private Vector3 _up;

        public override void Init(params object[] @params)
        {
            try {
                _healthProvider = @params[0] as IHealth;

                _canvas.worldCamera = Camera.main;
            }
            catch (Exception e) {
                ExceptionUI(e);
                return;
            }

            _canvas.transform.localScale = Vector3.one * _meterPerPixel;
            _up = this.transform.up;

            UpdateBar();
        }

        private void UpdateBar()
        {
            float currentHelah = _healthProvider.Health / _healthProvider.MaxHealth;
            _bar.fillAmount = currentHelah;

            int percentValue = Mathf.FloorToInt(currentHelah * 100);
            _percent.text = $"{percentValue}%";
        }

        private void FixedUpdate()
        {
            var camera = Camera.main;
            if (camera != null) {
                this.transform.LookAt(camera.transform.position, _up);
                this.transform.Rotate(_up, 180);
            }
        }

        private void OnBecameInvisible()
        {
            this.gameObject.SetActive(false);
        }

        private void OnBecameVisible()
        {
            this.gameObject.SetActive(true);
        }
    }
}