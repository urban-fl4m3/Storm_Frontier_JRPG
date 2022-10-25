using Source.Services.SmartCamera;
using UnityEngine;
using VContainer;

namespace Source.Installers.Common
{
    public class CameraInstaller : MonoBehaviour
    {
        [SerializeField] private Camera _activeCamera;

        private ICameraService _cameraService;
        
        [Inject]
        private void Construct(ICameraService cameraService)
        {
            _cameraService = cameraService;
        }

        private void Start()
        {
            _cameraService.SetActiveCamera(_activeCamera);
        }
    }
}