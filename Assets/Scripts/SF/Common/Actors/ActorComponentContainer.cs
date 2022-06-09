using System;
using System.Collections.Generic;
using SF.Game;
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

        public void InitActorComponents(IServiceLocator serviceLocator)
        {
            var actorComponents = GetComponentsInChildren<ActorComponent>();

            foreach (var actorComponent in actorComponents)
            {
                actorComponent.Init(Get<IActor>(), serviceLocator);
            }
        }

        private T CreateComponent<T>() where T : class
        {
            var component = GetComponentInChildren<T>();
            _components.Add(typeof(T), component);

            return component;
        }
    }
}