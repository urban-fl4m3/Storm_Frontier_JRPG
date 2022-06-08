using UnityEngine;

namespace SF.Common.Actors.Components.Transform
{
    public class TransformComponent : ActorComponent
    {
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}