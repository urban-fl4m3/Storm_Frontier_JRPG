using System.Collections.Generic;
using SF.Battle.Actors;
using SF.Battle.Common;
using SF.Common.Logger;
using SF.Common.Ticks;
using SF.Game;
using SF.Game.Common;
using SF.Game.Extensions;
using SF.UI.Models.Actions;
using UnityEngine;

namespace SF.Battle.Turns
{
    public class TurnManager
    {
        private readonly IDebugLogger _logger;
        private readonly ITickProcessor _tickProcessor;
        private readonly IBattleActorsHolder _actorsHolder;
        private readonly Dictionary<Team, ITurnAction> _turnActions = new();

        private ITurnAction _currentTurn;

        private readonly Dictionary<BattleActor, TurnActState> _actorStates = new();
        private readonly Queue<BattleActor> _actingActors = new();

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
            var actors = _actorsHolder.GetAllActors();
            
            foreach (var actor in actors)
            {
                if (!TryGetState(actor, out var state)) return;

                var actSpeed = actor.Stats.GetStat(state.Phase.GetPhaseFillStat());
                state.RaiseValue(actSpeed * Constants.Battle.ActionFillPerSpeed * Time.deltaTime);

                if (state.IsReadyPhase())
                {
                    _actingActors.Enqueue(actor);
                }
            }

            TryPlayNextTurn();
        }

        private bool TryGetState(BattleActor actor, out TurnActState currentState)
        {
            var isDead = actor.IsDead();

            if (_actorStates.TryGetValue(actor, out currentState))
            {
                if (isDead)
                {
                    _actorStates.Remove(actor);
                    return false;
                }
            }
            else
            {
                currentState = new TurnActState(Constants.Battle.ActionBarMeter);
                _actorStates.Add(actor, currentState);
            }

            return true;
        }

        private void TryPlayNextTurn()
        {
            if (_actingActors.TryDequeue(out var nextActor))
            {
                _tickProcessor.RemoveTick(OnBattleUpdate);
                PlayTurn(nextActor);
            }
            else
            {
                _tickProcessor.AddTick(OnBattleUpdate);
            }
        }
        
        private void PlayTurn(BattleActor actor)
        {
            var actingTeam = actor.Team;
        
            if (_turnActions.TryGetValue(actingTeam, out var turnAction) && !actor.IsDead())
            {
                _currentTurn = turnAction;
                _currentTurn.TurnCompleted += OnTurnCompleted;

                var state = _actorStates[actor];
                
                switch (state.Phase)
                {
                    case ActPhase.Wait:
                    {
                        PrepareAction(actor, state);
                        break;
                    }

                    case ActPhase.Cast:
                    {
                        Act(actor, state);;
                        break;
                    }
                }
            }
            else
            {
                _logger.LogWarning($"Team {actingTeam} for {actor} cant make turn...");
                OnTurnCompleted();
            }
        }  
        
        private void PrepareAction(BattleActor actor, TurnActState state)
        {
            //todo try and select cast
            _currentTurn.MakeTurn(actor);

            _currentTurn.ActionSelected += OnActionSelected;

            void OnActionSelected()
            {
                _currentTurn.ActionSelected -= OnActionSelected;
                state.SetCast(15);
            };
        }

        private void Act(BattleActor actor, TurnActState state)
        {
            //todo play animations etc
            _currentTurn.MakeTurn(actor);

            _currentTurn.TurnCompleted += OnActionCompleted;

            void OnActionCompleted()
            {
                _currentTurn.TurnCompleted -= OnActionCompleted;
                state.Refresh();
            }
        }
        
        private void OnTurnCompleted()
        {
            _currentTurn.TurnCompleted -= OnTurnCompleted;
            
            TryPlayNextTurn();
        }
    }
}