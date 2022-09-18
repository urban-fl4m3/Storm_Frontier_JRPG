using System;
using SF.Battle.Actors;
using SF.Battle.Common;
using SF.Common.Actors.Components.Status;
using SF.Game.Common;
using SF.Game.Extensions;
using UniRx;
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

        protected abstract IObservable<ITurnModel> OnTurnModelSelected { get; }

        private readonly float _maxWait;

        private float _currentStepProgress;
        private float _currentMax;
        private ITurnModel _currentTurnModel;
        private IDisposable _turnModelSelectionSub;

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
            var stateComponent = ActingActor.Components.Get<BattleStatusComponent>();

            return stateComponent.State.Value != ActorState.Dead;
        }

        public bool IsReadyPhase()
        {
            return _currentStepProgress >= _currentMax;
        }

        protected abstract void OnSelectionStepStart();
        protected abstract void OnSelectionStepFinish();
        protected abstract void OnActionStepStart();
        protected abstract void OnActionStepFinish();

        //cast start
        protected void SetActionTime(float actionTime)
        {
            _currentStepProgress = 0;
            _currentMax = actionTime;

            Phase = ActPhase.Action;
        }

        //cast end
        protected void Refresh()
        {
            _currentStepProgress = 0;
            _currentMax = _maxWait;

            Phase = ActPhase.Wait;
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

            _turnModelSelectionSub = OnTurnModelSelected.Subscribe(HandleTurnModelSelected);
        }

        private void ActionStep()
        {
            OnActionStepStart();

            if (_currentTurnModel != null)
            {
                _currentTurnModel.MakeTurnAction(HandleActionCompleted);
            }
            else
            {
                FailStep();
            }
        }

        private void HandleTurnModelSelected(ITurnModel model)
        {
            ClearTurnModel();
            _currentTurnModel = model;

            //raise Action Selected Event

            if (model == null)
            {
                return;
            }

            if (_currentTurnModel.IsTargetSelected())
            {
                HandleSelectedTarget(_currentTurnModel.GetCurrentTarget());
            }
            else
            {
                _currentTurnModel.TargetSelected += HandleSelectedTarget;
            }
        }

        private void HandleSelectedTarget(BattleActor target)
        {
            OnSelectionStepFinish();

            _turnModelSelectionSub?.Dispose();
            //raise Target Selected Event
            //take target
            CompleteStep();
        }

        private void HandleActionCompleted()
        {
            OnActionStepFinish();

            CompleteStep();
        }

        private void ClearTurnModel()
        {
            _currentTurnModel.TargetSelected -= HandleSelectedTarget;
            _currentTurnModel?.Dispose();
        }
    }
}