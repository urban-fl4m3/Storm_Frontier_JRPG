namespace SF.Common.Factories
{
    public interface IFactoryHolder
    {
        void Add(IFactory factory);
        
        TFactory Get<TFactory>() where TFactory : IFactory;
    }
}