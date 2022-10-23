using Source.Services;
using VContainer;
using VContainer.Unity;

namespace Source.Initializers
{
    public class BattleScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<TickService>(Lifetime.Scoped).AsSelf();
            // builder.Register<TickService>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
        }
    }
}