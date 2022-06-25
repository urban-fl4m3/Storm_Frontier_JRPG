using System.Collections.Generic;

namespace SF.Common.Data
{
    public interface IDataProvider
    {
        T GetData<T>();

        IEnumerable<object> GetAllData();
    }
}