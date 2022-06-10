using System.Threading;
using Cysharp.Threading.Tasks;
using SF.Battle.Actors;
using SF.Battle.TargetSelection;
using SF.Common.Actors;
using SF.Game;

namespace SF.Battle.Turns
{
    public class PlayerTurnModel
    {
        public BattleActor CurrentActor { get; set; }
        public IActor SelectedActor { get; private set; }
        public CancellationTokenSource CancelationToken { get; private set; }
        public UniTaskCompletionSource taskCompletionSource;

        private readonly BattleWorld _world;
        private ITargetSelectionRule _currentRule;

        public PlayerTurnModel(BattleWorld world)
        {
            _world = world;
        }

        public void SetSelectionRules(ITargetSelectionRule targetSelectionRule)
        {
            targetSelectionRule.TargetSelected += HandleTargetSelected;
            targetSelectionRule.TrackSelection(_world.ActingActors);

            void HandleTargetSelected(IActor target)
            {
                targetSelectionRule.TargetSelected -= HandleTargetSelected;
                SelectedActor = target;

                taskCompletionSource.TrySetResult();
            }
        }

        public void Cancel()
        {
            SelectedActor = null;

            CancelationToken?.Cancel();
            CancelationToken?.Dispose();

            CancelationToken = new CancellationTokenSource();
            taskCompletionSource = new UniTaskCompletionSource();
        }
    }
}