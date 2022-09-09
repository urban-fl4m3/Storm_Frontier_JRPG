using System;
using System.Collections.Generic;
using SF.Common.Logger;

namespace SF.Common.Camera
{
    public class SmartCameraRegistrar : ISmartCameraRegistrar
    {
        //move to config
        private const int MAIN_PRIORITY = 15;
        
        public event EventHandler<ISmartCamera> ObjectRegistered;
        public event EventHandler<ISmartCamera> ObjectUnregistered;

        private readonly IDebugLogger _logger;
        private readonly List<ISmartCamera> _cameras = new();

        private ISmartCamera _mainCamera;
        
        public SmartCameraRegistrar(IDebugLogger logger)
        {
            _logger = logger;
        }
        
        public bool Add(ISmartCamera obj)
        {
            _cameras.Add(obj);
            ObjectRegistered?.Invoke(this, obj);

            SetMainCamera(obj);
            
            return true;
        }

        public bool Remove(ISmartCamera obj)
        {
            var removed = _cameras.Remove(obj);

            if (!removed)
            {
                _logger.LogWarning($"Camera {obj} wasn't registered");
            }
            else
            {
                var camerasCount = _cameras.Count;

                if (camerasCount > 0)
                {
                    SetMainCamera(_cameras[^1]);
                }

                ObjectUnregistered?.Invoke(this, obj);
            }

            return removed;
        }

        public IEnumerable<ISmartCamera> GetAll()
        {
            return _cameras;
        }

        public ISmartCamera GetMainCamera()
        {
            return _mainCamera;
        }

        private void SetMainCamera(ISmartCamera camera)
        {
            _mainCamera?.SetPriority(0);

            camera.SetPriority(MAIN_PRIORITY);

            _mainCamera = camera;
        }
    }
}