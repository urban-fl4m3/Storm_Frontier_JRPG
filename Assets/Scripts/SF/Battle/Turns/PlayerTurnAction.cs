using System;
using Cysharp.Threading.Tasks;
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
        private readonly IReadonlyActionBinder _actionBinder;
        private readonly PlayerTurnModel _model;

        public PlayerTurnAction(IBattleActorsHolder actorsHolder, IReadonlyActionBinder actionBinder) : base(actorsHolder)
        {
            _actionBinder = actionBinder;
            _model = new PlayerTurnModel(actorsHolder);
        }

        protected override void OnStartTurn()
        {
            _actionBinder.Subscribe(ActionName.Attack, HandleAttackSelected);
            _actionBinder.Subscribe(ActionName.Skills, HandleSkillSelected);
            _actionBinder.Subscribe(ActionName.Item, HandleItemSelected);
            _actionBinder.Subscribe(ActionName.Guard, HandleGuardSelected);
        }

        protected override void OnStepFinished()
        {
            _actionBinder.Unsubscribe(ActionName.Attack, HandleAttackSelected);
            _actionBinder.Unsubscribe(ActionName.Skills, HandleSkillSelected);
            _actionBinder.Unsubscribe(ActionName.Item, HandleItemSelected);
            _actionBinder.Unsubscribe(ActionName.Guard, HandleGuardSelected);
         
            ClearModel();
        }

        private void HandleAttackSelected(IDataProvider dataProvider)
        {
            var attackSelectionData = new TargetSelectionData(TargetPick.OppositeTeam);
            var attackSelectionRule = new TargetSelectionRule(ActingActor, attackSelectionData);
            
            _selectedAction.OnNext(a =>
            {
                ActingActor.PerformAttack(a, CompleteStep);
            });
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
            
            MakeAsyncAction(skillSelectionRule,
                    (a) =>  ActingActor.PerformSkill(abilityData, a, CompleteStep))
                .Forget();
        }

        private void HandleItemSelected(IDataProvider dataProvider)
        {
            var itemIndex = dataProvider.GetData<int>();
            
            Debug.Log($"Item {itemIndex}!");
            CompleteStep();
        }
        
        private void HandleGuardSelected(IDataProvider dataProvider)
        {
            var guardSelectionData = new TargetSelectionData(TargetPick.Instant);
            var guardSelectionRule = new TargetSelectionRule(ActingActor, guardSelectionData);

            MakeAsyncAction(guardSelectionRule, 
                    () => ActingActor.PerformGuard(CompleteStep))
                .Forget();
        }

        private async UniTaskVoid MakeAsyncAction(ITargetSelectionRule selectionRule, Action<BattleActor> action)
        {
            ClearModel();
            _model.SetSelectionRules(selectionRule);
            
            SelectActor(null);
            
            await _model.TargetSelectedCompletionSource.Task;

            SelectActor(_model.SelectedActor);

            action?.Invoke();
        }

        private void ClearModel()
        {
            _model.Cancel();
            SelectActor(null);
        }
    }
}