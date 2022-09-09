using System.Threading;
using Cysharp.Threading.Tasks;
using SF.Battle.Actors;
using SF.Battle.Common;
using SF.Battle.TargetSelection;
using UnityEngine.InputSystem;

namespace SF.Battle.Turns
{
    public class PlayerTurnModel
    {
        public BattleActor SelectedActor { get; private set; }
        public UniTaskCompletionSource TargetSelectedCompletionSource { get; private set; }

        private readonly IBattleActorsHolder _actorsHolder;
        private readonly PlayerInputControls _playerInputControls;
        
        private CancellationTokenSource _cancelationToken;
        private BattleActor[] _possibleTargets;

        public PlayerTurnModel(IBattleActorsHolder actorsHolder)
        {
            _actorsHolder = actorsHolder;
            _playerInputControls = new PlayerInputControls();
        }

        public void SetSelectionRules(ITargetSelectionRule targetSelectionRule)
        {
            _playerInputControls.Battle.Targeting.performed += OnTargetChanged;
            _playerInputControls.Battle.Sumbit.performed += OnTargetSelected;
            
            _possibleTargets = targetSelectionRule.GetPossibleTargets(_actorsHolder.Actors);
            SelectedActor = _possibleTargets[0];
        }

        private void OnTargetSelected(InputAction.CallbackContext context)
        {
            TargetSelectedCompletionSource.TrySetResult();
        }

        private void OnTargetChanged(InputAction.CallbackContext context)
        {
            var nextActorSign = context.ReadValue<int>();
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