using System;
using Cysharp.Threading.Tasks;
using SF.Battle.Abilities;
using SF.Battle.Common;
using SF.Battle.Field;
using SF.Battle.TargetSelection;
using SF.Common.Actors;
using SF.Common.Actors.Abilities;
using SF.Common.Data;
using SF.Game;
using UnityEngine;

namespace SF.Battle.Turns
{
    public class PlayerTurnAction : BaseTurnAction
    {
        private readonly BattleField _field;
        private readonly PlayerTurnModel _model;

        public PlayerTurnAction(BattleField field, IBattleActorsHolder actorsHolder) : base(actorsHolder)
        {
            _field = field;
            _model = new PlayerTurnModel(actorsHolder);
        }

        protected override void OnStartTurn()
        {
            RenderActiveActor();
         
            ActingActor.SyncWith(_field.ActivePlayerPlaceholder);
        }

        protected override void OnTurnComplete()
        {
            
        }

        private void RenderActiveActor()
        {
            foreach (var actor in ActorsHolder.GetTeamActors(Team.Player))
            {
                var isActingActor = actor == ActingActor;
                actor.SetVisibility(isActingActor);
            }
        }

        public void HandleAttackSelected(IDataProvider dataProvider)
        {
            var attackSelectionData = new TargetSelectionData(TargetPick.OppositeTeam);
            var attackSelectionRule = new TargetSelectionRule(ActingActor, attackSelectionData);
            
            MakeAsyncAction(attackSelectionRule,  
                () => ActingActor.PerformAttack(_model.SelectedActor, CompleteTurn))
                .Forget();
        }

        public void HandleSkillSelected(IDataProvider dataProvider)
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
                () =>  ActingActor.PerformSkill(abilityData, _model.SelectedActor, CompleteTurn))
                .Forget();
        }

        public void HandleItemSelected(IDataProvider dataProvider)
        {
            var itemIndex = dataProvider.GetData<int>();
            
            Debug.Log($"Item {itemIndex}!");
            CompleteTurn();
        }
        
        public void HandleGuardSelected(IDataProvider dataProvider)
        {
            var guardSelectionData = new TargetSelectionData(TargetPick.Instant);
            var guardSelectionRule = new TargetSelectionRule(ActingActor, guardSelectionData);

            MakeAsyncAction(guardSelectionRule, 
                () => ActingActor.PerformGuard(CompleteTurn))
                .Forget();
        }

        private async UniTaskVoid MakeAsyncAction(ITargetSelectionRule selectionRule, Action action)
        {
            _model.Cancel();
            _model.SetSelectionRules(selectionRule);
            
            SelectActor(null);
            
            await _model.TargetSelectedCompletionSource.Task;

            SelectActor(_model.SelectedActor);

            action?.Invoke();
            ClearModel();
        }

        private void ClearModel()
        {
            _model.Cancel();
        }
    }
}