using Sirenix.OdinInspector;

namespace SF.Common.Actors
{
    public abstract class ActorComponent : SerializedMonoBehaviour, IActorComponent
    {
        public IActor Owner { get; private set; }
        
        public void Init(IActor owner)
        {
            Owner = owner;
        }
        
        public virtual void Enable()
        {
            
        }

        public virtual void Disable()
        {
            
        }
    }
}