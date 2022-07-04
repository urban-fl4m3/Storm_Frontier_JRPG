using UnityEngine;

namespace SF.Common.Actors
{
    public class CinemachineTargetComponent: ActorComponent
    {
        [SerializeField] private Transform _cameraPosition;
        [SerializeField] private Transform _lookAtPosition;

        public Transform CameraPosition => _cameraPosition;
        public Transform LookAtPosition => _lookAtPosition;
    }
}