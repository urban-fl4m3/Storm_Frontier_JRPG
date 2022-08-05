using SF.Battle.Common;
using SF.Common.Camera.Cinemachine;

namespace SF.Battle.Camera
{
    public class BattleCameraController : CinemachineController
    {
        public BattleCameraController(CinemachineView view, IBattleActorsHolder actorsHolder) : base(view)
        {
            
        }
        
        //Subscribe on acting actor
        private void SetupCamera()
        {
            // var cinemachineComponent = ActingActor.Components.Get<CinemachineTargetComponent>();
            //
            // //let's forget about camera here and let camera to position around field by himself
            // var camera = _cameraHolder.GetMainCamera();
            // camera.SetPosition(cinemachineComponent.CameraPosition);
            // camera.SetTarget(cinemachineComponent.LookAtPosition, 0);
            // camera.SetTarget(_field.Center, 1);
        }
    }
}