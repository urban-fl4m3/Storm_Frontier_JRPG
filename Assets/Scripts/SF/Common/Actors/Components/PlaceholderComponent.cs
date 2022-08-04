using UnityEngine;

namespace SF.Common.Actors
{
    public class PlaceholderComponent : ActorComponent
    {
        [SerializeField] private Transform frontPoint;
        [SerializeField] private GameObject _selectedCircle;

        public Transform FrontPoint => frontPoint;
        public Transform Placeholder => _placeholder;

        private Transform _placeholder;
        
        public void SetSelected(bool isSelected)
        {
            _selectedCircle.SetActive(isSelected);
        }

        public void SetPlaceholder(Transform placeholder)
        {
            _placeholder = placeholder;
        }
    }
}