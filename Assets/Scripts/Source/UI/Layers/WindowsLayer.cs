using Source.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Source.UI.Layers
{
    [RequireComponent(typeof(Canvas))]
    public class WindowsLayer : MonoBehaviour, IStartable
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
        public void Construct(CameraService cameraService)
        {
            _cameraService = cameraService;
        }

        public void Start()
        {
            Canvas.worldCamera = _cameraService.ActiveCamera;
        }
    }
}