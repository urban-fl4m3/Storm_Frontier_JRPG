using SF.Common.Camera;
using SF.Common.Factories;
using SF.Common.Logger;
using SF.Common.Ticks;

namespace SF.Game
{
    public interface IServiceLocator
    {
        IDebugLogger Logger { get; }
        ITickProcessor TickProcessor { get; }
        IFactoryHolder FactoryHolder { get; }
        ISmartCameraRegistrar CameraHolder { get; }
    }
}