using System;
using Cysharp.Threading.Tasks;
using SF.Battle.Actors;
using SF.Battle.Common;
using SF.Common.Actors.Components.Status;
using SF.Game.Common;
using SF.Game.Extensions;
using UnityEngine;

namespace SF.Battle.Turns
{
    public abstract class BaseTurnAction : ITurnAction
    {
        public event Action StepCompleted;
        public event Action StepFailed;

        public ActPhase Phase { get; private set; }
        protected BattleActor ActingActor { get; }
        protected IBattleActorsHolder ActorsHolder { get; }
        
        private readonly float _maxWait;

        private float _currentStepProgress;
        private float _currentMax;

        protected BaseTurnAction(BattleActor actingActor, IBattleActorsHolder actorsHolder)
        {
            ActingActor = actingActor;
            ActorsHolder = actorsHolder;

            _maxWait = Constants.Battle.ActionBarMeter;
            _currentMax = _maxWait;
            _currentStepProgress = 0;

            Phase = ActPhase.Wait;
        }

        public void NextStep()
        {
            if (!CanPerformStep())
            {
                FailStep();
                return;
            }
            
            switch (Phase)
            {
                case ActPhase.Wait:
                {
                    SelectionStep().Forget();
                    break;
                }

                case ActPhase.Action:
                {
                    ActionStep().Forget();
                    break;
                }
            } 
        }
        
        public void RaiseStepProgress()
        { 
            var actSpeed = ActingActor.Stats.GetStat(Phase.GetPhaseFillStat());
            var deltaSpeed = actSpeed * Constants.Battle.ActionFillPerSpeed * Time.deltaTime;
            
            var newStepProgress = _currentStepProgress + deltaSpeed;

            if (Phase == ActPhase.Action)
            {
                if (newStepProgress <= 0)
                {
                    Phase = ActPhase.Wait;
                
                    _currentMax = _maxWait;
                    newStepProgress = _currentMax + newStepProgress;
                    
                }
            }
            
            _currentStepProgress = Mathf.Clamp(newStepProgress, 0, _currentMax);
        }
        
        public bool CanPerformStep()
        {
            var stateComponent = ActingActor.Components.Get<BattleStatusComponent>();

            return stateComponent.State.Value != ActorState.Dead;
        }

        public bool IsReadyPhase()
        {
            return _currentStepProgress >= _currentMax;
        }

        protected abstract void OnStartTurn();

        protected abstract void OnStepFinished();

        protected void CompleteStep()
        {
            OnStepFinished();
            
            StepCompleted?.Invoke();
        }

        protected void FailStep()
        {
            OnStepFinished();
            
            StepFailed?.Invoke();
        }

        //after 
        protected void SetActionTime(float actionTime)
        {
            _currentStepProgress = 0;
            _currentMax = actionTime;
            
            Phase = ActPhase.Action;
        }
        
        protected void Refresh()
        {
            _currentStepProgress = 0;
            _currentMax = _maxWait;
            
            Phase = ActPhase.Wait;
        }

        private async UniTaskVoid SelectionStep()
        {
            var selectedAction = await GetSelectedAction();
        }

        private async UniTaskVoid ActionStep()
        {
        }

        protected abstract UniTask<BattleActor> GetSelectedTarget();
        protected abstract UniTask<Action<BattleActor>> GetSelectedAction();
    }
}