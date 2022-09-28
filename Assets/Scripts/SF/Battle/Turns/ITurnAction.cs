using System;
using SF.Battle.Actors;

namespace SF.Battle.Turns
{
    public interface ITurnAction
    {
        ActPhase Phase { get; }
        BattleActor ActingActor { get; }
        
        event Action StepCompleted;
        event Action StepFailed;

        void NextStep();
        bool IsReadyPhase();
        bool CanPerformStep();
        void RaiseStepProgress();
        float GetCurrentProgress();
        float GetMaxProgress();
    }
}