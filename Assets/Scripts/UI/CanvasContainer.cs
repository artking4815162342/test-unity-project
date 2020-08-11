using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public sealed class CanvasContainer : BaseMonoSinglton<CanvasContainer>
    {
        [SerializeField]
        private Canvas _canvas;

        public Transform MainTransform => _canvas.transform;
    }
}