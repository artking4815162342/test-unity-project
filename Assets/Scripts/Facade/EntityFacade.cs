namespace Game.Facade
{
    public sealed class EntityFacade 
    {
        private EntityEventProvider _eventProvider;
        private EntityAggregator _entityAggregator;

        public EntityFacade()
        {
            _eventProvider = new EntityEventProvider();
            _entityAggregator = new EntityAggregator();

            _eventProvider.Subscribe(_entityAggregator);
        }

        public IEntityAggregator EntityAggregator => _entityAggregator;

        public IEntityEventProvider EntityEventProvider => _eventProvider;
    }
}