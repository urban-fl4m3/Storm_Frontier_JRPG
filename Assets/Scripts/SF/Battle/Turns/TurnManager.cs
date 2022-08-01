using System.Collections.Generic;
using System.Linq;
using SF.Battle.Actors;
using SF.Game;
using SF.UI.Controller;

namespace SF.Battle.Turns
{
    public class TurnManager
    {
        private readonly BattleWorld _world;
        private readonly IServiceLocator _serviceLocator;
        private readonly Dictionary<Team, ITurnAction> _turnActions;
        private readonly Queue<BattleActor> _waitingActors = new Queue<BattleActor>();

        private ITurnAction _currentTurn;
        
        public TurnManager(IServiceLocator serviceLocator, BattleWorld world, PlayerActionsViewController playerActionsViewController)
        {
            _serviceLocator = serviceLocator;
            _world = world;

            _turnActions = new Dictionary<Team, ITurnAction>
            {
                {Team.Player, new PlayerTurnAction(world.Field, serviceLocator.CameraHolder, world.Actors, playerActionsViewController)},
                {Team.Enemy, new AiTurnAction(serviceLocator.Logger, world.Actors, serviceLocator.CameraHolder)}
            };
        }

        public void PlayNextTurn()
        {
            ValidateQueue();

            var actor = _waitingActors.Dequeue();

            var actingTeam = actor.Team;

            if (_turnActions.TryGetValue(actingTeam, out var turnAction))
            {
                _currentTurn = turnAction;
                
                _currentTurn.TurnCompleted += OnTurnCompleted;
                _currentTurn.MakeTurn(actor);
            }
            else
            {
                _serviceLocator.Logger.LogWarning($"Team {actingTeam} for {actor} cant make turn...");
                OnTurnCompleted();
            }
        }

        private void OnTurnCompleted()
        {
            _currentTurn.TurnCompleted -= OnTurnCompleted;
            PlayNextTurn();
        }
        
        private void ValidateQueue()
        {
            if (_waitingActors.Count != 0) return;

            var sortedByTeamActors = _world.Actors.ActingActors.OrderBy(x => x.Team);

            foreach (var actor in sortedByTeamActors)
            {
                _waitingActors.Enqueue(actor);
            }
        }
    }
}