using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public interface IElementUI
    {
        void Init(params object[] @params);
    }

    public abstract class BaseUIElement : MonoBehaviour, IElementUI
    {
        public abstract void Init(params object[] @params);

        protected void ExceptionUI(System.Exception exception)
        {
            //TODO : change to logger
            Debug.LogException(exception);
        }
    }
}