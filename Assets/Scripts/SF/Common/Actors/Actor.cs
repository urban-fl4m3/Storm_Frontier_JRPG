using Sirenix.OdinInspector;
using UnityEngine;

namespace SF.Common.Actors
{
    [RequireComponent(typeof(ActorComponentContainer))]
    public class Actor : SerializedMonoBehaviour, IActor
    {
        public ActorComponentContainer Components { get; private set; }
        
        public void Init()
        {
            Components = GetComponent<ActorComponentContainer>();

            var actorComponents = GetComponentsInChildren<ActorComponent>();

            foreach (var actorComponent in actorComponents)
            {
                actorComponent.Init(this);
            }
        }

        public virtual void Enable()
        {
            
        }

        public virtual void Disable()
        {
            
        }
    }
}