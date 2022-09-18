using SF.Battle.Abilities;
using SF.Battle.Actors;
using SF.Battle.Common;
using SF.Battle.TargetSelection;
using SF.Common.Actors.Abilities;
using SF.Common.Data;
using SF.Game.Common;
using SF.UI.Models.Actions;
using Sirenix.Utilities;
using UnityEngine.InputSystem;

namespace SF.Battle.Turns
{
    public class PlayerTurnAction : BaseTurnAction
    {
        private readonly IReadonlyActionBinder _actionBinder;
        private readonly PlayerInputControls _playerInputControls = new();  //todo add input manager to service locator

        private BattleActor[] _possibleTargets;

        
        public PlayerTurnAction(BattleActor actor, IBattleActorsHolder actorsHolder, IReadonlyActionBinder actionBinder) 
            : base(actor, actorsHolder)
        {
            _actionBinder = actionBinder;
        }

        protected override void OnSelectionStepStart()
        {
            _actionBinder.Subscribe(ActionName.Attack, HandleAttackSelected);
            _actionBinder.Subscribe(ActionName.Skills, HandleSkillSelected);
            _actionBinder.Subscribe(ActionName.Item, HandleItemSelected);
            _actionBinder.Subscribe(ActionName.Guard, HandleGuardSelected);
        }

        protected override void OnSelectionStepFinish()
        {
            _actionBinder.Unsubscribe(ActionName.Attack, HandleAttackSelected);
            _actionBinder.Unsubscribe(ActionName.Skills, HandleSkillSelected);
            _actionBinder.Unsubscribe(ActionName.Item, HandleItemSelected);
            _actionBinder.Unsubscribe(ActionName.Guard, HandleGuardSelected);
            
            ClearInputSubs();
        }

        protected override void OnActionStepStart()
        {
            
        }

        protected override void OnActionStepFinish()
        {
            
        }

        private void HandleAttackSelected(IDataProvider dataProvider)
        {
            var attackSelectionData = new TargetSelectionData(TargetPick.OppositeTeam);
            var attackSelectionRule = new TargetSelectionRule(ActingActor, attackSelectionData);
            
            SelectActionToPerform(ActingActor.PerformAttack);
            SetActionTime(Constants.Battle.MinCastTime);
            StartTargetSelection(attackSelectionRule);
        }

        private void HandleSkillSelected(IDataProvider dataProvider)
        {
            var abilityData = dataProvider.GetData<ActiveBattleAbilityData>();

            if (abilityData == null)
            {
                return;
            }
            
            var abilityComponent = ActingActor.Components.Get<AbilityComponent>();

            if (!abilityComponent.CanInvoke(abilityData))
            {
                return;
            }

            var skillSelectionData = new TargetSelectionData(abilityData.Pick);
            var skillSelectionRule = new TargetSelectionRule(ActingActor, skillSelectionData);

            SelectActionToPerform(a => ActingActor.PerformSkill(abilityData, a));
            SetActionTime(abilityData.CastTime);
            StartTargetSelection(skillSelectionRule);
        }

        private void HandleItemSelected(IDataProvider dataProvider)
        {
            var itemIndex = dataProvider.GetData<int>();
            
            var itemSelectionData = new TargetSelectionData(TargetPick.Any);
            var itemSelectionRule = new TargetSelectionRule(ActingActor, itemSelectionData);

            SelectActionToPerform(a => ActingActor.PerformUseItem(itemIndex, a));
            SetActionTime(Constants.Battle.MinCastTime);
            StartTargetSelection(itemSelectionRule);
        }
        
        private void HandleGuardSelected(IDataProvider dataProvider)
        {
            var guardSelectionData = new TargetSelectionData(TargetPick.Instant);
            var guardSelectionRule = new TargetSelectionRule(ActingActor, guardSelectionData);

            SelectActionToPerform(a => ActingActor.PerformGuard());
            SetActionTime(Constants.Battle.MinCastTime);
            StartTargetSelection(guardSelectionRule);
        }

        private void StartTargetSelection(ITargetSelectionRule rules)
        {
            ClearInputSubs();
            
            _playerInputControls.Battle.Targeting.performed += HandleTargetChanged;
            _playerInputControls.Battle.Sumbit.performed += HandleTargetSelected;
            
            _possibleTargets = rules.GetPossibleTargets(ActorsHolder.GetAllActors());

            var hasPossibleTargets = !_possibleTargets.IsNullOrEmpty();
            
            if (hasPossibleTargets)
            {
                PickTarget(_possibleTargets[0]);
            }

            RaiseActionSelected(!hasPossibleTargets);
        }   

        private void HandleTargetChanged(InputAction.CallbackContext context)
        {
            var nextActorSign = context.ReadValue<int>();
            
            //if sign > 0 - get next target
            //if sign < 0 - get previous target
            
            //PickTarget
        }
        
        private void HandleTargetSelected(InputAction.CallbackContext context)
        {
            SelectPickedTarget();
        }

        private void ClearInputSubs()
        {
            _playerInputControls.Battle.Targeting.performed -= HandleTargetChanged;
            _playerInputControls.Battle.Sumbit.performed -= HandleTargetSelected;
        }
    }
}