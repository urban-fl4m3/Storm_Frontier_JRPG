using UnityEngine;

namespace SF.Common.Actors
{
    public class CurrentPlayerComponent: ActorComponent
    {
        [SerializeField] private GameObject _selectedCircle;

        public void SetSelected(bool isSelected)
        {
            _selectedCircle.SetActive(isSelected);
        }
    }
}