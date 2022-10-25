using Source.Common.States;
using Source.Game.States.Concrete;
using VContainer;

namespace Source.Game.States
{
    public static class StatesInstaller
    {
        public static void Install(IContainerBuilder builder)
        {
            builder
                .Register<CharacterSelectionGateEntity>(Lifetime.Scoped)
                .As<IState>();

            builder
                .Register<BattleState>(Lifetime.Scoped)
                .As<IState>();
        }
    }
}