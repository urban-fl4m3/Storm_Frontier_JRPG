using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IReadonlyActionBinder _actionBinder;
        private readonly IBattleActorsHolder _actorsHolder;

        private readonly List<ITurnAction> _registeredActions = new();
        private readonly Queue<ITurnAction> _actionsToProceed = new();

        public TurnManager(
            IDebugLogger logger,
            ITickProcessor tickProcessor,
            IReadonlyActionBinder actionBinder,
            IBattleActorsHolder actorsHolder)
        {
            _logger = logger;
            _actorsHolder = actorsHolder;
            _tickProcessor = tickProcessor;
            _actionBinder = actionBinder;

            foreach (var actor in actorsHolder.GetAllActors())
            {
                HandleAddedActor(actor);
            }
        }
        
        public void Enable()
        {
            _tickProcessor.AddTick(OnBattleUpdate);
        }

        private void HandleAddedActor(BattleActor actor)
        {
            //todo create SF team exception
            ITurnAction action = actor.Team switch
            {
                Team.Player => new PlayerTurnAction(actor, _actorsHolder, _actionBinder),
                Team.Enemy => new AiTurnAction(_logger, _actorsHolder),
                _ => throw new ArgumentOutOfRangeException()
            };
            
            _registeredActions.Add(action);
        }
        
        private void OnBattleUpdate(long delta)
        {
            //todo maybe we should hide bool check in calculations, like if actor are dead or stunned raise + 0
            var viableActions = _registeredActions.Where(a => a.CanPerformStep());
            
            foreach (var action in viableActions)
            {
                action.RaiseStepProgress();
                
                if (action.IsReadyPhase())
                {
                    _actionsToProceed.Enqueue(action);
                }
            }

            TryPlayNextTurn();
        }

        private void TryPlayNextTurn()
        {
            if (_actionsToProceed.TryDequeue(out var nextAction))
            {
                _tickProcessor.RemoveTick(OnBattleUpdate);
                PlayTurn(nextAction);
            }
            else
            {
                _tickProcessor.AddTick(OnBattleUpdate);
            }
        }
        
        private void PlayTurn(ITurnAction action)
        {
            action.StepCompleted += OnStepCompleted;
            action.StepFailed += OnStepFailed;
            action.NextStep();
            
            void OnStepCompleted()
            {
                action.StepCompleted -= OnStepCompleted;
                action.StepFailed -= OnStepFailed;
            
                TryPlayNextTurn();
            }

            void OnStepFailed()
            {
                action.StepCompleted -= OnStepCompleted;
                action.StepFailed -= OnStepFailed;
            
                TryPlayNextTurn();
                _logger.LogWarning($"Step failed");
            }
        }
    }
}