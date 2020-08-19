using System;
using UnityEngine;
using UnityEngine.UI;
using Game.Entity;
using TMPro;

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

        [Header("Animation")]
        [SerializeField]
        private TextMeshProUGUI _textMesh;

        [SerializeField]
        private RectTransform _animationRect;

        [Range(0f, 500f)]
        [SerializeField]
        private float _animTopPoint;

        [Range(0.6f, 2f)]
        [SerializeField]
        private float _animSpeed;

        private IHealth _healthProvider;
        private Vector3 _up;

        private int _lastHealthValue = 0;
        private float _animationTime = 0f;

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
            _lastHealthValue = _healthProvider.Health;

            _healthProvider.HealthChange += OnHealthChange;

            _textMesh.gameObject.SetActive(false);

            UpdateBar();
        }

        private void FixedUpdate()
        {
            var camera = Camera.main;
            if (camera != null) {
                this.transform.LookAt(camera.transform.position, _up);
                this.transform.Rotate(_up, 180);

                Animation();
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

        private void OnHealthChange()
        {
            UpdateBar();
            PlayAnimation();
        }

        private void UpdateBar()
        {
            float currentHelah =
                (float)_healthProvider.Health / _healthProvider.MaxHealth;
            _bar.fillAmount = currentHelah;

            _percent.text = _healthProvider.Health.ToString();
        }

        private void PlayAnimation()
        {
            var delta = _healthProvider.Health - _lastHealthValue;

            _textMesh.text = delta.ToString();
            _textMesh.gameObject.SetActive(true);

            _animationTime = 0f;
            _animationRect.anchoredPosition = Vector2.zero;

            _lastHealthValue = _healthProvider.Health;
        }

        private void Animation()
        {
            if (_textMesh.gameObject.activeSelf == false) {
                return;
            }

            _animationTime += Time.deltaTime * _animSpeed;
            _animationRect.anchoredPosition = 
                Vector2.Lerp(Vector2.zero, new Vector2(0, _animTopPoint), _animationTime);

            if (_animationRect.anchoredPosition.y >= _animTopPoint) {
                _textMesh.gameObject.SetActive(false);
            }
        }
    }
}