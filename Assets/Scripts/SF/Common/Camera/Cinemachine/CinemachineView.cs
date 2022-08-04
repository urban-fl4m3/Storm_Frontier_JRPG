using Cinemachine;
using UnityEngine;

namespace SF.Common.Camera.Cinemachine
{
    public class CinemachineView : MonoBehaviour
    {
        [SerializeField] private CinemachineTargetGroup _targetGroup;
        [SerializeField] private CinemachineVirtualCamera _camera;

        public void MoveCamera(Transform target)
        {
            _camera.transform.position = target.transform.position;
            _camera.transform.SetParent(target);
        }

        public void SetFollower(Transform follower)
        {
            _camera.Follow = follower;
        }

        public void SetTarget(Transform target, int index)
        {
            if (_targetGroup.m_Targets.Length <= index)
            {
                _targetGroup.AddMember(target, 1, 0);
            }
            
            _targetGroup.m_Targets[index].target = target;
        }

        public void SetPriority(int priority)
        {
            _camera.Priority = priority;
        }

        public void Clear()
        {
            _targetGroup.m_Targets[^1].target = null;
        }
    }
}