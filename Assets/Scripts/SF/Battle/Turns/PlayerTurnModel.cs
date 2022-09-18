using System;
using SF.Battle.Actors;
using SF.Battle.Common;
using SF.Battle.TargetSelection;
using Sirenix.Utilities;
using UnityEngine.InputSystem;

namespace SF.Battle.Turns
{
    public class PlayerTurnModel : ITurnModel
    {
        public event Action<BattleActor> TargetSelected = delegate { };
        public event Action<BattleActor> TargetPicked = delegate { };

        private readonly BattleActor _actingActor;
        private readonly ITargetSelectionRule _rules;
        private readonly Action<BattleActor> _turnAction;
        private readonly IBattleActorsHolder _actorsHolder;
        private readonly PlayerInputControls _playerInputControls;
        
        private BattleActor[] _possibleTargets;
        private BattleActor _pickedTarget;
        private bool _isTargetSelected;

        public PlayerTurnModel(
            BattleActor actingActor, 
            IBattleActorsHolder actorsHolder,
            ITargetSelectionRule rules,
            Action<BattleActor> turnAction)
        {
            _rules = rules;
            _turnAction = turnAction;
            _actingActor = actingActor;
            _actorsHolder = actorsHolder;
            _playerInputControls = new PlayerInputControls();
            
            Init();
        }

        public void MakeTurnAction(Action onActionCompleted)
        {
            var target = _pickedTarget;

            if (target == null || target.IsDead())
            {
                //pick next possible target
            }

            _actingActor.ActionPerformed += HandleActionPerformed; 
            _turnAction?.Invoke(target);

            void HandleActionPerformed()
            {
                _actingActor.ActionPerformed -= HandleActionPerformed; 
                onActionCompleted?.Invoke();
            }
        }

        public float GetActionCost()
        {
            
        }

        public bool IsTargetSelected()
        {
            return _isTargetSelected;
        }

        public BattleActor GetCurrentTarget()
        {
            return _pickedTarget;
        }

        public void Dispose()
        {
            _playerInputControls.Battle.Targeting.performed -= OnTargetChanged;
            _playerInputControls.Battle.Sumbit.performed -= OnTargetSelected;
        }

        private void Init()
        {
            _playerInputControls.Battle.Targeting.performed += OnTargetChanged;
            _playerInputControls.Battle.Sumbit.performed += OnTargetSelected;
            
            _possibleTargets = _rules.GetPossibleTargets(_actorsHolder.GetAllActors());

            var hasPossibleTargets = !_possibleTargets.IsNullOrEmpty();
            
            if (hasPossibleTargets)
            {
                ChangePickedTarget(_possibleTargets[0]);
            }

            _isTargetSelected = hasPossibleTargets;
        }

        private void ChangePickedTarget(BattleActor target)
        {
            _pickedTarget = target;
            TargetPicked(target);
        }
        
        private void OnTargetChanged(InputAction.CallbackContext context)
        {
            var nextActorSign = context.ReadValue<int>();
            
            //if sign > 0 - get next target
            //if sign < 0 - get previous target
        }
        
        private void OnTargetSelected(InputAction.CallbackContext context)
        {
            _isTargetSelected = true;
            TargetSelected(_pickedTarget);
        }
    }
}