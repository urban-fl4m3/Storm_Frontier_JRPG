using Source.Common.States;
using Source.Game.States.Concrete;
using VContainer.Unity;

namespace Source.Game
{
    public class GameInitializer : IStartable
    {
        private readonly IChangeStateResolver _stateResolver;

        public GameInitializer(IChangeStateResolver stateResolver)
        {
            _stateResolver = stateResolver;
        }

        void IStartable.Start()
        {
            _stateResolver.ChangeState<BattleState>();
        }
    }
}