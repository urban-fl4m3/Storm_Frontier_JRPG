using Source.Initializers;
using Source.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Source.UI.Layers
{
    [RequireComponent(typeof(Canvas))]
    public class WindowsLayer : MonoBehaviour, IStartable, ITickable
    {
        private Canvas Canvas
        {
            get
            {
                if (_canvas == null)
                {
                    _canvas = GetComponent<Canvas>();
                }

                return _canvas;
            }
        }
        
        private Canvas _canvas;
        private CameraService _cameraService;

        [Inject]
        public void Construct(CameraService cameraService, TickService tickService)
        {
            _cameraService = cameraService;
        }

        void IStartable.Start()
        {
            Canvas.worldCamera = _cameraService.ActiveCamera;
        }

        void ITickable.Tick()
        {
            Debug.Log("DD");
        }
    }
}