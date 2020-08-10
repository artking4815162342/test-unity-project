using System;
using System.Collections;
using System.Collections.Generic;
using Game.Entity;

namespace Game.Facade
{
    public interface IEntityEventProvider
    {
        void OnChangeExistanceStatus(EntityEventArgs e);
    }

    public sealed partial class EntityEventProvider : IObservable<EntityEventArgs>, IEntityEventProvider
    {
        private List<IObserver<EntityEventArgs>> _observers;

        public EntityEventProvider()
        {
            _observers = new List<IObserver<EntityEventArgs>>();
        }

        public IDisposable Subscribe(IObserver<EntityEventArgs> observer)
        {
            if (!_observers.Contains(observer))
                _observers.Add(observer);
            return new Unsubscriber(_observers, observer);
        }

        public void OnChangeExistanceStatus(EntityEventArgs e)
        {
            foreach (var observer in _observers) {
                if (e == null) {
                    observer.OnError(new ArgumentNullException());
                }
                else {
                    observer.OnNext(e);
                }
            }
        }
    }

    public sealed partial class EntityEventProvider : IObservable<EntityEventArgs>, IEntityEventProvider
    {
        private sealed class Unsubscriber : IDisposable
        {
            private List<IObserver<EntityEventArgs>> _observers;
            private IObserver<EntityEventArgs> _observer;

            public Unsubscriber(
                List<IObserver<EntityEventArgs>> observers,
                IObserver<EntityEventArgs> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer)) {
                    _observers.Remove(_observer);
                }
            }
        }
    }

    public sealed class EntityEventArgs : EventArgs
    {
        public BaseSceneEntity Entity { get; private set; }

        public bool ExistanceStatus { get; private set; }

        public EntityEventArgs(BaseSceneEntity entity, bool existanceStatus)
        {
            Entity = entity;
            ExistanceStatus = existanceStatus;
        }
    }
}
