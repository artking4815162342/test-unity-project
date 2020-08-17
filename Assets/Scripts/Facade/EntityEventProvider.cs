using System;
using System.Collections;
using System.Collections.Generic;
using Game.Entity;
using static Game.Facade.EntityEventProvider;

namespace Game.Facade
{
    public interface IEntityEventProvider
    {
        void SendEvent(EntityEventArgs e);

        IDisposable Subscribe(IObserver<EntityEventArgs> observer);

        IDisposable Subscribe(IObserver<EntityEventArgs> observer, EventType type);
    }

    public sealed partial class EntityEventProvider : IObservable<EntityEventArgs>, IEntityEventProvider
    {
        public enum EventType
        {
            Default,
        }

        private Dictionary<EventType, List<IObserver<EntityEventArgs>>> _observersMap;

        private List<IObserver<EntityEventArgs>> DefaultObservers => _observersMap[EventType.Default];

        public EntityEventProvider()
        {
            _observersMap = new Dictionary<EventType, List<IObserver<EntityEventArgs>>>();
            _observersMap.Add(EventType.Default, new List<IObserver<EntityEventArgs>>());
        }

        public IDisposable Subscribe(IObserver<EntityEventArgs> observer)
        {
            if (!DefaultObservers.Contains(observer))
                DefaultObservers.Add(observer);
            return new Unsubscriber(DefaultObservers, observer);
        }

        public IDisposable Subscribe(IObserver<EntityEventArgs> observer, EventType type)
        {
            if (_observersMap.ContainsKey(type)) {
                _observersMap[type].Add(observer);
            }
            else {
                var observers = new List<IObserver<EntityEventArgs>>();
                observers.Add(observer);
                _observersMap.Add(type, observers);
            }

            return new Unsubscriber(_observersMap[type], observer);
        }

        public void SendEvent(EntityEventArgs e)
        {
            if (!_observersMap.TryGetValue(e.Type, out var observers)) {
                return;
            }

            foreach (var observer in observers) {
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
        public EventType Type { get; private set; }

        public BaseSceneEntity Entity { get; private set; }

        public bool Is { get; private set; }

        public EntityEventArgs(BaseSceneEntity entity, bool @is, EventType type = EventType.Default) 
            : this(entity, type)
        {
             Is = @is;
        }

        public EntityEventArgs(BaseSceneEntity entity, EventType type = EventType.Default)
        {
            Entity = entity;
        }
    }
}