using UnityEngine;

namespace SF.Common.Camera
{
    public interface ISmartCamera
    {
        void SetFollower(Transform follower);
        void SetMainTarget(Transform target);
        void SetTarget(Transform target, int index);
        void SetPosition(Transform target);
        void SetPriority(int priority);
        void Clear();
    }
}