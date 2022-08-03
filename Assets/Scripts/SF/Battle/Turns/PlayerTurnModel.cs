using System.Threading;
using Cysharp.Threading.Tasks;
using SF.Battle.Actors;
using SF.Battle.Common;
using SF.Battle.TargetSelection;

namespace SF.Battle.Turns
{
    public class PlayerTurnModel
    {
        public BattleActor SelectedActor { get; private set; }
        public UniTaskCompletionSource TargetSelectedCompletionSource { get; private set; }

        private readonly IRegisteredActorsHolder _actorsHolder;
        
        private ITargetSelectionRule _currentRule;
        private CancellationTokenSource _cancelationToken;

        public PlayerTurnModel(IRegisteredActorsHolder actorsHolder)
        {
            _actorsHolder = actorsHolder;
        }

        public void SetSelectionRules(ITargetSelectionRule targetSelectionRule)
        {
            targetSelectionRule.TargetSelected += HandleTargetSelected;
            targetSelectionRule.TrackSelection(_actorsHolder.ActingActors);

            void HandleTargetSelected(BattleActor target)
            {
                targetSelectionRule.TargetSelected -= HandleTargetSelected;
                SelectedActor = target;

                TargetSelectedCompletionSource.TrySetResult();
            }
        }

        public void Cancel()
        {
            SelectedActor = null;

            _cancelationToken?.Cancel();
            _cancelationToken?.Dispose();

            _cancelationToken = new CancellationTokenSource();
            TargetSelectedCompletionSource = new UniTaskCompletionSource();
        }
    }
}