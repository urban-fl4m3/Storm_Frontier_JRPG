using UnityEngine;

namespace SF.Common.Actors.Components
{
    public class RotationComponent : ActorComponent
    {
        public void LookAt(Vector3 lookAtVector)
        {
            transform.rotation = Quaternion.LookRotation(lookAtVector, transform.up);
        }
    }
}