public sealed class EntityFacade
{
    public IMobController MobController { get; private set; }

    public EntityFacade(IMobController mobController)
    {
        MobController = mobController;
    }
}