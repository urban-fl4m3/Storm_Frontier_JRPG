using System.Collections.Generic;
using SF.Battle.Actors;
using SF.Battle.Common;
using SF.Common.Logger;
using SF.Common.Ticks;
using SF.Game;
using SF.UI.Models.Actions;

namespace SF.Battle.Turns
{
    public class TurnManager
    {
        private readonly IDebugLogger _logger;
        private readonly ITickProcessor _tickProcessor;
        private readonly IBattleActorsHolder _actorsHolder;
        private readonly Dictionary<Team, ITurnAction> _turnActions = new();

        private ITurnAction _currentTurn;
        private IEnumerable<BattleActor> _actingActors;
        
        public TurnManager(IDebugLogger logger, ITickProcessor tickProcessor, IReadonlyActionBinder actionBinder, IBattleActorsHolder actorsHolder)
        {
            _logger = logger;
            _actorsHolder = actorsHolder;
            _tickProcessor = tickProcessor;
            
            _turnActions.Add(Team.Player, new PlayerTurnAction(_actorsHolder, actionBinder));
            _turnActions.Add(Team.Enemy, new AiTurnAction(logger, _actorsHolder));

        }

        public void Enable()
        {
            _tickProcessor.AddTick(OnBattleUpdate);
        }

        private void OnBattleUpdate(long delta)
        {
            
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
        
        private void OnTurnCompleted()
        {
            _currentTurn.TurnCompleted -= OnTurnCompleted;
            PlayNextTurn();
        }
    }
}