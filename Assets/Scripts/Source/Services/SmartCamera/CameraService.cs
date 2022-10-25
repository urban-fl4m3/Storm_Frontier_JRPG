using UnityEngine;

namespace Source.Services.SmartCamera
{
    public class CameraService : ICameraService
    {
        private Camera _activeCamera;
        
        public void SetActiveCamera(Camera camera)
        {
            _activeCamera = camera;
        }
    }
}