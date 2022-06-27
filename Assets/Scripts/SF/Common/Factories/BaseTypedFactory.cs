using System;
using System.Collections.Generic;
using SF.Common.Data;

namespace SF.Common.Factories
{
    public abstract class BaseTypedFactory<T1, T2> : IFactory<T1, T2> where T2 : IFactoryInstance
    {
        protected abstract Dictionary<Type, Type> DiscriminatedTypes { get; }
        
        public T2 Create(T1 obj, IDataProvider dataProvider = null)
        {
            var t = obj.GetType();

            if (DiscriminatedTypes.ContainsKey(t))
            {
                var instanceType = DiscriminatedTypes[t];
                var instance = (T2) Activator.CreateInstance(instanceType);
                OnInstantiate(obj, instance, dataProvider);

                return instance;
            }

            return default;
        }

        protected virtual void OnInstantiate(T1 from, T2 instance, IDataProvider dataProvider)
        {
            instance.SetFactoryMeta(dataProvider);
        }
    }
}