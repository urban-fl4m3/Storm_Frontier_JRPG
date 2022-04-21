using UnityEngine;

namespace SF.Common.Actors.Components
{
    public class TransformComponent : ActorComponent
    {
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    }
}