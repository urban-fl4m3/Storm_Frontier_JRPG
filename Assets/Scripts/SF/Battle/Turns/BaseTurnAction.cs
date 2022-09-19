using System;
using SF.Battle.Actors;
using SF.Battle.Common;
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
        private IDisposable _turnModelSelectionSub;

        private BattleActor _pickedTarget;
        private Action<BattleActor> _selectedAction;
        
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
                    SelectionStep();
                    break;
                }

                case ActPhase.Action:
                {
                    ActionStep();
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
            return !ActingActor.IsDead();
        }

        public bool IsReadyPhase()
        {
            return _currentStepProgress >= _currentMax;
        }

        protected abstract void OnSelectionStepStart();
        protected abstract void OnSelectionStepFinish();
        protected abstract void OnActionStepStart();
        protected abstract void OnActionStepFinish();
        
        protected void RaiseActionSelected(bool isTargetSelected)
        {
            //todo raise Action Selected Event

            if (isTargetSelected)
            {
                SelectPickedTarget();
            }
        }

        protected void PickTarget(BattleActor actor)
        {
            _pickedTarget = actor;
        }

        protected void SelectActionToPerform(Action<BattleActor> action)
        {
            _selectedAction = action;
        }

        protected void SelectPickedTarget()
        {
            OnSelectionStepFinish();

            _turnModelSelectionSub?.Dispose();
            //todo raise Target Selected Event
            //todo take target from picked target
            CompleteStep();
        }
        
        protected void SetActionTime(float actionTime)
        {
            _currentStepProgress = 0;
            _currentMax = actionTime;

            Phase = ActPhase.Action;
        }

        private void CompleteStep()
        {
            StepCompleted?.Invoke();
        }

        private void FailStep()
        {
            StepFailed?.Invoke();
        }

        private void SelectionStep()
        {
            OnSelectionStepStart();
        }

        private void ActionStep()
        {
            OnActionStepStart();
            MakeTurnAction();
        }

        private void MakeTurnAction()
        {
            var target = _pickedTarget;

            if (target == null || target.IsDead())
            {
                //todo pick next possible target
            }

            ActingActor.ActionPerformed += HandleActionPerformed; 
            _selectedAction?.Invoke(target);

            void HandleActionPerformed()
            {
                ActingActor.ActionPerformed -= HandleActionPerformed; 
                
                OnActionStepFinish();
                Refresh();
                CompleteStep();
            }
        }
        
        private void Refresh()
        {
            _currentStepProgress = 0;
            _currentMax = _maxWait;

            Phase = ActPhase.Wait;
        }
    }
}