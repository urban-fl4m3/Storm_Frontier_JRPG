namespace SF.Game
{
    public abstract class BaseWorld : IWorld
    {
        protected IServiceLocator ServiceLocator { get; }

        protected BaseWorld(IServiceLocator serviceLocator)
        {
            ServiceLocator = serviceLocator;
        }
    }
}