using UnityEngine;

namespace SF.Common.Camera.Cinemachine
{
    public class CinemachineController : ISmartCamera
    {
        private const int MAIN_TARGET_INDEX = 0;
        
        private readonly CinemachineView _view;

        public CinemachineController(CinemachineView view)
        {
            _view = view;
        }
        
        public void SetFollower(Transform follower)
        {
            _view.SetFollower(follower);
        }

        public void SetMainTarget(Transform target)
        {
            SetTarget(target, MAIN_TARGET_INDEX);
        }

        public void SetTarget(Transform target, int index)
        {
            _view.SetTarget(target, index);
        }

        public void SetPosition(Transform target)
        {
            _view.MoveCamera(target);
        }

        public void SetPriority(int priority)
        {
            _view.SetPriority(priority);
        }

        public void Clear()
        {
            _view.Clear();
        }
    }
}