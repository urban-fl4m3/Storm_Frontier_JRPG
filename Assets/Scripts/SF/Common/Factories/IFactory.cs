using SF.Common.Data;

namespace SF.Common.Factories
{
    public interface IFactory
    {
        
    }
    
    public interface IFactory<in T1, out T2> : IFactory where T2 : IFactoryInstance
    {
        T2 Create(T1 obj, IDataProvider dataProvider = null);
    }
}