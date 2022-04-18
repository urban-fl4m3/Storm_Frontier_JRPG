using SF.Game;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SF.Common.Actors
{
    [RequireComponent(typeof(ActorComponentContainer))]
    public class Actor : SerializedMonoBehaviour, IActor
    {
        public ActorComponentContainer Components { get; private set; }
        
        protected IServiceLocator ServiceLocator { get; private set; }
        
        public void Init(IServiceLocator serviceLocator)
        {
            ServiceLocator = serviceLocator;
            Components = GetComponent<ActorComponentContainer>();
            InitComponents();
        }

        public virtual void Enable()
        {
            
        }

        public virtual void Disable()
        {
            
        }

        private void InitComponents()
        {
            var actorComponents = GetComponentsInChildren<ActorComponent>();

            foreach (var actorComponent in actorComponents)
            {
                actorComponent.Init(this);
            }            
        }
    }
}