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
            Components.InitActorComponents();
        }

        public virtual void Enable()
        {
            
        }

        public virtual void Disable()
        {
            
        }
    }
}