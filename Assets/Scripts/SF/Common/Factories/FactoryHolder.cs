using System;
using System.Collections.Generic;

namespace SF.Common.Factories
{
    public class FactoryHolder : IFactoryHolder
    {
        private readonly Dictionary<Type, IFactory> _factories = new();

        public void Add(IFactory factory)
        {
            _factories.Add(factory.GetType(), factory);   
        }

        public TFactory Get<TFactory>() where TFactory : IFactory
        {
            return (TFactory) _factories[typeof(TFactory)];
        }
    }
}