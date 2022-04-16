using System;
using System.Collections.Generic;
using UnityEngine;

namespace SF.Common.Actors
{
    public class ActorComponentContainer : MonoBehaviour
    {
        private readonly Dictionary<Type, object> _components = new Dictionary<Type, object>();
        
        public T Get<T>() where T : class
        {
            var type = typeof(T);

            var hasComponent = _components.TryGetValue(type, out var component);

            if (hasComponent)
            {
                return (T)component;
            }
            
            return CreateComponent<T>();
        }

        private T CreateComponent<T>() where T : class
        {
            var component = GetComponentInChildren<T>();
            _components.Add(typeof(T), component);

            return component;
        }
    }
}