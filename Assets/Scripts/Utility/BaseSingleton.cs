public abstract class BaseSingleton<T> where T : BaseSingleton<T>, new()
{
    public static T Get
    {
        get
        {
            if (_instance == null) {
                _instance = new T();
            }

            return _instance;
        }
    }

    private static T _instance;
}