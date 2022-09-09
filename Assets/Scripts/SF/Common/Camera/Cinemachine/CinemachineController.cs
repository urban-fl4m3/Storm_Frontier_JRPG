using UnityEngine;

namespace SF.Common.Camera.Cinemachine
{
    public class CinemachineController : ISmartCamera
    {
        private readonly CinemachineView _view;

        public CinemachineController(CinemachineView view)
        {
            _view = view;
        }
        
        public void SetFollower(Transform follower)
        {
            _view.SetFollower(follower);
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