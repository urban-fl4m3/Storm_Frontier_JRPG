namespace SF.Common.Data
{
    public interface IDataProvider
    {
        T GetData<T>();
    }
}