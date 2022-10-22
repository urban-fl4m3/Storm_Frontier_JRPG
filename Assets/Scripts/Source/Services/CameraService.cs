using UnityEngine;

namespace Source.Services
{
    public class CameraService
    {
        public Camera ActiveCamera { get; }
        
        public CameraService(Camera camera)
        {
            ActiveCamera = camera;
        }
    }
}