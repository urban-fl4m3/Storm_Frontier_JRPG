using System.Collections.Generic;
using System.Linq;
using SF.Battle.Actors;
using SF.Game;
using UniRx;

namespace SF.Battle.Turns
{
    public class TurnManager
    {
        public IReadOnlyReactiveProperty<BattleActor> ActiveActor => _activeActor;

        private readonly BattleWorld _world;
        private readonly IServiceLocator _serviceLocator;
        private readonly Dictionary<Team, ITurnAction> _turnActions;

        private readonly Queue<BattleActor> _waitingActors = new Queue<BattleActor>();
        private readonly ReactiveProperty<BattleActor> _activeActor = new ReactiveProperty<BattleActor>();

        public TurnManager(IServiceLocator serviceLocator, BattleWorld world)
        {
            _serviceLocator = serviceLocator;
            _world = world;

            _turnActions = new Dictionary<Team, ITurnAction>
            {
                {Team.Player, new PlayerTurnAction(serviceLocator, world)},
                {Team.Enemy, new AiTurnAction(serviceLocator, world)}
            };
        }

        public void PlayNextTurn()
        {
            ValidateQueue();

            var actor = _waitingActors.Dequeue();
            _activeActor.Value = actor;

            var actingTeam = actor.Team;

            if (_turnActions.TryGetValue(actingTeam, out var turnAction))
            {
                turnAction.TurnCompleted += OnTurnCompleted;
                turnAction.MakeTurn(actor);
            }
            else
            {
                _serviceLocator.Logger.LogWarning($"Team {actingTeam} for {ActiveActor} cant make turn...");
                OnTurnCompleted();
            }

            void OnTurnCompleted()
            {
                turnAction.TurnCompleted -= OnTurnCompleted;
                PlayNextTurn();
            }
        }

        private void ValidateQueue()
        {
            if (_waitingActors.Count != 0) return;

            var sortedByTeamActors = _world.ActingActors.OrderBy(x => x.Team);

            foreach (var actor in sortedByTeamActors)
            {
                _waitingActors.Enqueue(actor);
            }
        }
    }
}