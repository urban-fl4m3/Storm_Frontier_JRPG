using System;
using UnityEngine.EventSystems;

namespace SF.Common.Actors
{
    public class ActorSelectComponent: ActorComponent, IPointerClickHandler
    {
        public event Action<IActor> ActorSelected = delegate { };

        public void Clear()
        {
            ActorSelected = delegate { };
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            ActorSelected.Invoke(Owner);
        }
    }
}