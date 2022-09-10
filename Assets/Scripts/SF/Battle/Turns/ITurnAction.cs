using System;

namespace SF.Battle.Turns
{
    public interface ITurnAction
    {
        public ActPhase Phase { get; }

        event Action StepCompleted;
        event Action StepFailed;

        void NextStep();
        bool IsReadyPhase();
        bool CanPerformStep();
        void RaiseStepProgress();
        
    }
}