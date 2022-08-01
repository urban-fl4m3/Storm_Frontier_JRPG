using SF.Common.Registerers;

namespace SF.Common.Camera
{
    public interface ISmartCameraRegistrar : IRegistrar<ISmartCamera>
    {
        ISmartCamera GetMainCamera();
    }
}