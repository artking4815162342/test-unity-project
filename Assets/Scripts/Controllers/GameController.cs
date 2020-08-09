public sealed class GameController : BaseSingleton<GameController>
{
    public EntityFacade EntityFacade { get; private set; }

    public GameController()
    {
        EntityFacade = new EntityFacade(new MobController());
    }
}
