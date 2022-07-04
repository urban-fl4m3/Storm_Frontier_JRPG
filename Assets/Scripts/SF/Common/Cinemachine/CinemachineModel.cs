using System;
using UnityEngine;

namespace SF.Common.Cinemachine
{
    public class CinemachineModel
    {
        public event Action<Transform> CameraPositionSet = delegate { };
        public event Action<Transform, int> TargetSet = delegate { };
        public event Action Clear = delegate { };

        public void OnClear()
        {
            Clear.Invoke();
        }

        public void OnSetTarget(Transform target, int index)
        {
            TargetSet.Invoke(target, index);
        }

        public void OnSetCameraPosition( Transform target)
        {
            CameraPositionSet.Invoke(target);
        }
        
    }
}