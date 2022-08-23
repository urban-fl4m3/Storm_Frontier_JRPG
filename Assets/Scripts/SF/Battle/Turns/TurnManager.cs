using System.Collections.Generic;
using SF.Battle.Common;
using SF.Common.Logger;
using SF.Game;

namespace SF.Battle.Turns
{
    public class TurnManager
    {
        private readonly IDebugLogger _logger;
        private readonly IBattleActorsHolder _actorsHolder;
        private readonly Dictionary<Team, ITurnAction> _turnActions = new();

        private ITurnAction _currentTurn;
        
        public TurnManager(IDebugLogger logger, IBattleActorsHolder actorsHolder)
        {
            _logger = logger;
            _actorsHolder = actorsHolder;
        }

        public void PlayNextTurn()
        {
            var actor = _actorsHolder.ActingActor;

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

        public void BindAction(Team team, ITurnAction action)
        {
            if (!_turnActions.ContainsKey(team))
            {
                _turnActions.Add(team, action);
            }
        }

        public ITurnAction GetTurnAction(Team team)
        {
            return _turnActions[team];
        }

        private void OnTurnCompleted()
        {
            _actorsHolder.SetNextActingActor();
            _currentTurn.TurnCompleted -= OnTurnCompleted;
            PlayNextTurn();
        }
    }
}