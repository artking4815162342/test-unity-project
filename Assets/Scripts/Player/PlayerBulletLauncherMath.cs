using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.GeneralModule;

namespace Game.PlayerController
{
    public sealed partial class PlayerBulletLauncher : BaseModule, IBulletLanuncher
    {
        private readonly int _dotsCount = 20;
        private readonly int _angleDegree = -30;
        private readonly float _gravity = 9.80665f;

        private float _alphaRad;
        private float sinAlpha;
        private float cosAlpha;
        private float tgAlpha;

        private void DrowProcess()
        {
            Vector3 cameraPos = Camera.main.transform.position;
            Vector3 mousePos = Input.mousePosition;
            Vector3 mousePos3D = new Vector3(
                mousePos.x, mousePos.y, cameraPos.z);

            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos3D);
            Vector3 direction = mouseWorldPos - cameraPos;

#if UNITY_EDITOR
            //ray from camera to collision point
            Debug.DrawRay(cameraPos, direction, Color.red);
#endif
            if (Physics.Raycast(cameraPos, direction, out var hit, float.MaxValue, _mask)) {
                _collisionPoint.position = hit.point;
                _collisionPoint.transform.rotation =
                    Quaternion.FromToRotation(Vector3.forward, hit.normal);

                Vector3 fullVector = hit.point - _parent.position;
                Vector2 horizontalPart = new Vector2(fullVector.x, fullVector.z);
                var @params = CalcParams(horizontalPart.magnitude, fullVector.y);

                _parent.LookAt(hit.point);
                Vector3 velocityDirection = _parent.forward;

                Quaternion velocityRotation = Quaternion.AngleAxis(_angleDegree, _parent.right);
                velocityDirection.y = 0;
                velocityDirection = velocityRotation * velocityDirection;

                _resultForce = velocityDirection.normalized * @params.velocity;

#if UNITY_EDITOR
                //ray with initial velocity direction
                Debug.DrawRay(_parent.position, velocityDirection.normalized * 10, Color.black);
#endif

                Vector3 drowDirection = fullVector;
                float partLength = drowDirection.magnitude / _listPoints.Count;

                drowDirection = drowDirection.normalized * partLength;

                _listPoints[0].localPosition = Vector3.zero;

                for (int i = 1; i < _listPoints.Count; i++) {
                    Vector3 newLinearPosition =
                        _listPoints[i - 1].position + drowDirection;
                    _listPoints[i].position = newLinearPosition;

                    var currentHorizontalLocalVector = (_listPoints[i].position - _parent.position);
                    currentHorizontalLocalVector.y = 0;
                    var currentLocalLength = currentHorizontalLocalVector.magnitude;

                    var newLocalPosY = CalcYPosition(currentLocalLength, @params.velocity);

                    newLinearPosition.y = _parent.position.y + newLocalPosY;
                    _listPoints[i].position = newLinearPosition;
                }
            }
        }

        /// <summary>
        /// Calc initial velocity and full time from equation 
        /// of motion of a body thrown at an angle to the horizon.
        /// </summary>
        /// <returns></returns>
        private (float velocity, float time) CalcParams(float x, float y)
        {
            (float velocity, float time) result;

            result.velocity = Mathf.Sqrt((_gravity * x * x) / (2 * cosAlpha * cosAlpha * (x * tgAlpha - y)));
            result.time = x / (result.velocity * cosAlpha);

            return result;
        }

        /// <summary>
        /// Calc vertical local position from equation 
        /// of motion of a body thrown at an angle to the horizon.
        /// </summary>
        /// <returns></returns>
        private float CalcYPosition(float x, float velocity)
        {
            return x * tgAlpha - (_gravity * x * x) / (2 * velocity * velocity * cosAlpha * cosAlpha);
        }

        private void CalcTrigonometricConstants()
        {
            _alphaRad = Mathf.Abs(_angleDegree) * Mathf.Deg2Rad;

            sinAlpha = Mathf.Sin(_alphaRad);
            cosAlpha = Mathf.Cos(_alphaRad);
            tgAlpha = sinAlpha / cosAlpha;
        }
    }
}