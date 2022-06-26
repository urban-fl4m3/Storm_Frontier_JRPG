namespace SF.Common.Factories
{
    public interface IFactoryHolder
    {
        void Add<TFactory>(TFactory factory) where TFactory : IFactory;
        
        TFactory Get<TFactory>() where TFactory : IFactory;
    }
}