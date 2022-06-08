using UnityEngine;

namespace SF.Common.Actors
{
    public class PlaceholderComponent: MonoBehaviour
    {
        [SerializeField] private Transform _point;
        
        public Transform Point => _point;
    }
}