using System.Collections.Generic;
using System.Linq;
using SF.Battle.Actors;
using SF.Game;

namespace SF.Battle.Turns
{
    public class TurnManager
    {
        private readonly BattleWorld _world;
        private readonly IServiceLocator _serviceLocator;
        private readonly Dictionary<Team, ITurnAction> _turnActions;
        private readonly Queue<BattleActor> _waitingActors = new Queue<BattleActor>();

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

            var actingActor = _waitingActors.Dequeue();
            var actingTeam = actingActor.Team;

            if (_turnActions.TryGetValue(actingTeam, out var turnAction))
            {
                turnAction.TurnCompleted += OnTurnCompleted;
                turnAction.MakeTurn(actingActor);
            }
            else
            {
                _serviceLocator.Logger.LogWarning($"Team {actingTeam} for {actingActor} cant make turn...");
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