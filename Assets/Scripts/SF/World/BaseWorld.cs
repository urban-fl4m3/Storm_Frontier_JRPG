namespace SF.Game
{
    public abstract class BaseWorld : IWorld
    {
        public IServiceLocator ServiceLocator { get; }

        protected BaseWorld(IServiceLocator serviceLocator)
        {
            ServiceLocator = serviceLocator;
        }
    }
}