using Game.Facade;

public sealed class GameInfrastructure : BaseSingleton<GameInfrastructure>
{
    public EntityFacade EntityFacade { get; private set; }

    public GameInfrastructure()
    {
        EntityFacade = new EntityFacade();
    }
}
