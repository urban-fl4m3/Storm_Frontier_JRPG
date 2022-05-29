using System;

namespace SF.Common.Actors
{
    public class ActorSelectComponent : ActorComponent
    {
        public event Action<IActor> ActorSelected = delegate { };

        public void OnMouseEnter()
        {
            ActorSelected.Invoke(Owner);
        }
    }
}