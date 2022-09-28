using UnityEngine;

namespace SF.Common.Actors
{
    public class PlaceholderComponent : ActorComponent
    {
        [SerializeField] private Transform frontPoint;
        [SerializeField] private GameObject _selectedCircle;

        public Transform Placeholder { get; set; }
        public Transform FrontPoint => frontPoint;

        private Transform _placeholder;
        
        public void SetSelected(bool isSelected)
        {
            _selectedCircle.SetActive(isSelected);
        }
    }
}