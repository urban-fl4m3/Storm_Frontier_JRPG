using UnityEngine;

namespace SF.Common.Camera
{
    public interface ISmartCamera
    {
        void SetFollower(Transform follower);
        void SetPosition(Transform target);
        void SetPriority(int priority);
        void Clear();
    }
}