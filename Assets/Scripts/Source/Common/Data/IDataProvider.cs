using System.Collections.Generic;

namespace Source.Common.Data
{
    public interface IDataProvider
    {
        T GetData<T>();

        IEnumerable<object> GetAllData();
    }
}