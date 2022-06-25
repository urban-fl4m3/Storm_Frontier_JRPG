using System;
using System.Collections.Generic;

namespace SF.Common.Factories
{
    public class FactoryHolder : IFactoryHolder
    {
        private readonly Dictionary<Type, IFactory> _factories = new Dictionary<Type, IFactory>();

        public void Add<TFactory>(TFactory factory) where TFactory : IFactory
        {
            _factories.Add(typeof(TFactory), factory);   
        }

        public TFactory Get<TFactory>() where TFactory : IFactory
        {
            return (TFactory) _factories[typeof(TFactory)];
        }
    }
}