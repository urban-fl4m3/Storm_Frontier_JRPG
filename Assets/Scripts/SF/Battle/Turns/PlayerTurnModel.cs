using System.Threading;
using SF.Battle.Actors;
using SF.Battle.TargetSelection;
using SF.Game;

namespace SF.Battle.Turns
{
    public class PlayerTurnModel
    {
        public BattleActor CurrentActor { get; set; }
        public BattleActor SelectedActor { get; private set; }
        public CancellationTokenSource CancelationToken { get; private set; }
        
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

            void HandleTargetSelected(BattleActor target)
            {
                targetSelectionRule.TargetSelected -= HandleTargetSelected;
                SelectedActor = target;
            }
        }

        public void Cancel()
        {
            SelectedActor = null;
            
            CancelationToken?.Cancel();
            CancelationToken?.Dispose();

            CancelationToken = new CancellationTokenSource();
        }
    }
}