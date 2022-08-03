using System.Collections.Generic;
using System.Linq;
using SF.Battle.Actors;
using SF.Battle.Common;
using SF.Battle.Field;
using SF.Common.Camera;
using SF.Common.Logger;
using SF.Game;
using SF.UI.Controller;

namespace SF.Battle.Turns
{
    public class TurnManager
    {
        private readonly IDebugLogger _logger;
        private readonly IRegisteredActorsHolder _actorsHolder;
        private readonly Queue<BattleActor> _waitingActors = new();
        private readonly Dictionary<Team, ITurnAction> _turnActions;

        private ITurnAction _currentTurn;
        
        public TurnManager(
            IDebugLogger logger, 
            BattleField field,
            ISmartCameraRegistrar cameraHolder,
            IRegisteredActorsHolder actorsHolder,
            PlayerActionsViewController playerActionsViewController)
        {
            _logger = logger;
            _actorsHolder = actorsHolder;
            
            _turnActions = new Dictionary<Team, ITurnAction>
            {
                {Team.Player, new PlayerTurnAction(field, cameraHolder, actorsHolder, playerActionsViewController)},
                {Team.Enemy, new AiTurnAction(logger, actorsHolder, cameraHolder)}
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
                _logger.LogWarning($"Team {actingTeam} for {actor} cant make turn...");
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

            var sortedByTeamActors = _actorsHolder.ActingActors.OrderBy(x => x.Team);

            foreach (var actor in sortedByTeamActors)
            {
                _waitingActors.Enqueue(actor);
            }
        }
    }
}