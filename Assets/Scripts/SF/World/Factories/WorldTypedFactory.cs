using System;
using System.Collections.Generic;
using System.Linq;
using SF.Common.Data;
using SF.Common.Factories;

namespace SF.Game.Factories
{
    public abstract class WorldTypedFactory<T1, T2> : BaseTypedFactory<T1, T2> where T2 : IFactoryInstance
    {
        protected abstract override Dictionary<Type, Type> DiscriminatedTypes { get; }

        public T2 Create(T1 obj, IWorld world, IDataProvider dataProvider = null)
        {
            return Create(obj, GetCombinedDataProvider(world, dataProvider));
        }

        private IDataProvider GetCombinedDataProvider(IWorld world, IDataProvider dataProvider)
        {
            IDataProvider combinedDataProvider;

            if (dataProvider != null)
            {
                var allData = dataProvider.GetAllData();

                combinedDataProvider = new DataProvider(world, allData.Select(x => x));
            }
            else
            {
                combinedDataProvider = new DataProvider(world);
            }

            return combinedDataProvider;
        }
    }
}