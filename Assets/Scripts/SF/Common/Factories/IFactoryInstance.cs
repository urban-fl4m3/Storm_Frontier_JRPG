using SF.Common.Data;

namespace SF.Common.Factories
{
    public interface IFactoryInstance
    {
        void SetFactoryMeta(IDataProvider dataProvider);
    }
}