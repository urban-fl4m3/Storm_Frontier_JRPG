using UnityEngine;

namespace SF.Common.Actors.Factories
{
    public struct ActorSpawnData
    {
        public Vector3 Position { get; }
        public Quaternion Rotation { get; }
        public Transform Parent { get; }
        
        public ActorSpawnData(Vector3 position, Quaternion rotation, Transform parent = null)
        {
            Position = position;
            Rotation = rotation;
            Parent = parent;
        }

        public static ActorSpawnData Default()
        {
            return new ActorSpawnData(Vector3.zero, Quaternion.identity);
        }

        public static ActorSpawnData Positioning(Vector3 position, Transform parent = null)
        {
            return new ActorSpawnData(position, Quaternion.identity, parent);
        }
    }
}