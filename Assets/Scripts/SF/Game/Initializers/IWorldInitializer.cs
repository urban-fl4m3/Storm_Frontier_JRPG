using SF.Common.Data;

namespace SF.Game.Initializers
{
    public interface IWorldInitializer
    {
        IDataProvider GetWorldData();
    }
}