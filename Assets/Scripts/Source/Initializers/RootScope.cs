using Source.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Source.Initializers
{
    public class RootScope : LifetimeScope
    {
        [SerializeField] private Camera _camera;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(new CameraService(_camera));

        
            // builder.RegisterEntryPoint<TickService>();
            builder.RegisterEntryPoint<GameInitializer>(Lifetime.Scoped);
        }
    }
}