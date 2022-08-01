using UnityEngine;

namespace SF.Common.Actors
{
    public class ViewComponent : ActorComponent
    {
        [SerializeField] private GameObject _view;

        public bool IsVisible
        {
            set => _view.gameObject.SetActive(value);
        }
    }
}