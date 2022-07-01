using UnityEngine;

namespace SF.Common.Actors
{
    public class PlaceholderComponent: MonoBehaviour
    {
        [SerializeField] private Transform _point;
        [SerializeField] private GameObject _selectedCircle;

        public Transform Point => _point;
        
        public void SetSelected(bool isSelected)
        {
            _selectedCircle.SetActive(isSelected);
        }
    }
}