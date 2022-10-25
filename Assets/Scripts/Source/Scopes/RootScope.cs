using Source.Common.States;
using Source.Game;
using Source.Game.States;
using Source.Services;
using VContainer;
using VContainer.Unity;

namespace Source.Scopes
{
    public class RootScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            ServicesInstaller.Install(builder);
            StatesInstaller.Install(builder);

            builder
                .Register<GameStateMachine>(Lifetime.Scoped)
                .As<IChangeStateResolver>();
            
            builder.RegisterEntryPoint<GameInitializer>(Lifetime.Scoped);
        }
    }
}