using System;
using SF.Battle.Abilities;
using SF.Battle.Actors;
using SF.Battle.Common;
using SF.Battle.TargetSelection;
using SF.Common.Actors.Abilities;
using SF.Common.Data;
using SF.UI.Models.Actions;
using UniRx;
using UnityEngine;

namespace SF.Battle.Turns
{
    public class PlayerTurnAction : BaseTurnAction
    {
        protected override IObservable<ITurnModel> OnTurnModelSelected => _onTurnModelSelected;

        private readonly ISubject<ITurnModel> _onTurnModelSelected = new Subject<ITurnModel>();
        private readonly IReadonlyActionBinder _actionBinder;

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

            var model = new PlayerTurnModel(ActingActor, ActorsHolder, attackSelectionRule, actor =>
            {
                ActingActor.PerformAttack(actor);
            });
            
            _onTurnModelSelected.OnNext(model);
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

            var model = new PlayerTurnModel(ActingActor, ActorsHolder, skillSelectionRule, actor =>
            {
                ActingActor.PerformSkill(abilityData, actor);
            });
            
            _onTurnModelSelected.OnNext(model);
        }

        private void HandleItemSelected(IDataProvider dataProvider)
        {
            var itemIndex = dataProvider.GetData<int>();
            
            Debug.Log($"Item {itemIndex}!");

            var model = new MockTurnModel();
            
            _onTurnModelSelected.OnNext(model);
        }
        
        private void HandleGuardSelected(IDataProvider dataProvider)
        {
            var guardSelectionData = new TargetSelectionData(TargetPick.Instant);
            var guardSelectionRule = new TargetSelectionRule(ActingActor, guardSelectionData);

            var model = new PlayerTurnModel(ActingActor, ActorsHolder, guardSelectionRule, _ =>
            {
                ActingActor.PerformGuard();
            });
            
            _onTurnModelSelected.OnNext(model);
        }
    }
}