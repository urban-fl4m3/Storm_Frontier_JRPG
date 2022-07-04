using System.Linq;
using Cinemachine;
using UnityEngine;

namespace SF.Common.Cinemachine
{
    public class CinemachineView : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private CinemachineTargetGroup _targetGroup;
        [SerializeField] private Transform _centralPosition;

        public void MoveCamera(Transform target)
        {
            _camera.transform.position = target.transform.position;
            _camera.transform.SetParent(target);
        }

        public void SetTargetGroup(Transform target, int index)
        {
            _targetGroup.m_Targets[index].target = target;
        }

        public void Clear()
        {
            _targetGroup.m_Targets[_targetGroup.m_Targets.Length - 1].target = _centralPosition;
        }
    }
}