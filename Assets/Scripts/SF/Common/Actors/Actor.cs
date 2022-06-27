using SF.Game;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SF.Common.Actors
{
    [RequireComponent(typeof(ActorComponentContainer))]
    public class Actor : SerializedMonoBehaviour, IActor
    {
        public ActorComponentContainer Components { get; private set; }
        public IWorld World { get; private set; }

        protected IServiceLocator ServiceLocator { get; private set; }

        public virtual void Enable()
        {
            
        }

        public virtual void Disable()
        {
            
        }
        
        protected void Init(IServiceLocator serviceLocator, IWorld world)
        {
            World = world;
            ServiceLocator = serviceLocator;
            Components = GetComponent<ActorComponentContainer>();
            Components.InitActorComponents(serviceLocator);
        }
    }
}