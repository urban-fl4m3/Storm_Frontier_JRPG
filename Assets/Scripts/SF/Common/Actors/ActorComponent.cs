using Sirenix.OdinInspector;

namespace SF.Common.Actors
{
    public abstract class ActorComponent : SerializedMonoBehaviour, IActorComponent
    {
        protected IActor Owner { get; private set; }
        
        public void Init(IActor owner)
        {
            Owner = owner;
        }
    }
}