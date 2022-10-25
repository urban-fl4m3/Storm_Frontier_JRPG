using Source.Services.Loggers;
using Source.Services.SmartCamera;
using Source.Services.Ticks;
using VContainer;
using VContainer.Unity;

namespace Source.Services
{
    public static class ServicesInstaller
    {
        public static void Install(IContainerBuilder builder)
        {
            builder
                .Register<CameraService>(Lifetime.Singleton)
                .As<ICameraService>();

            builder
                .Register<TickService>(Lifetime.Singleton)
                .As<ITickable>()
                .As<ITickService>();

            builder
                .Register<DebugLogger>(Lifetime.Singleton)
                .As<IDebugLogger>();
        }
    }
}