using System;
using System.Linq;
using Cysharp.Threading.Tasks;
using SF.Battle.Actors;
using SF.Common.Actors;
using SF.Common.Actors.Components;
using SF.Common.Animations;
using SF.Game;
using SF.UI.Controller;
using Sirenix.Utilities;
using UnityEngine;

namespace SF.Battle.Turns
{
    public class PlayerTurnAction : BaseTurnAction
    {
        private readonly BattleHUDController _battleHUDController;
        private readonly PlayerTurnModel _model;

        private Actor _actingActor;
        
        public PlayerTurnAction(IServiceLocator services, BattleWorld world, BattleHUDController battleHUDController, PlayerTurnModel model) 
            : base(services, world)
        {
            _battleHUDController = battleHUDController;
            _model = model;
        }
        
        public override void MakeTurn(BattleActor actor)
        {
            _actingActor = actor;
            _model.SetCurrentActor(actor);
            
            _battleHUDController.ShowHUD();
            
            _battleHUDController.AttackSelected += HandleAttackSelected;
            _battleHUDController.SkillSelected += HandleSkillSelected;
            _battleHUDController.ItemSelected += HandleItemSelected;
            _battleHUDController.GuardSelected += HandleGuardSelected;
        }

        private void HandleAttackSelected()
        {
            _model.Cancel();
            SetSelectedPlayer();
            
            AttackAsync().Forget();
        }

        private async UniTask AttackAsync()
        {
            
            await UniTask.WaitWhile(() => _model.SelectedActor is null, cancellationToken: _model.CancelationToken.Token);

            var currentActorTransform = _model.CurrentActor.transform;
            var animationComponent = _model.CurrentActor.Components.Get<AnimationComponent>();
            var place = _model.SelectedActor.Components.Get<PlaceholderComponent>().Point;
            var startPlace = currentActorTransform.transform.position;
            var eventHandler = _model.CurrentActor.Components.Get<AnimationEventHandler>();

            currentActorTransform.position = place.transform.position;
            animationComponent.SetAttackTrigger();
            eventHandler.Subscribe("AttackEvent", CompleteAttack);
            
            void CompleteAttack(object sender, EventArgs e)
            {
                currentActorTransform.transform.position = startPlace;
                
                eventHandler.Unsubscribe("AttackEvent", CompleteAttack);
                
                CompleteTurn();
            }
        }
        
        private void SetSelectedPlayer()
        {
            var enemies = World.ActingActors.Where(x => x.Team == Team.Enemy);
            var battleActors = enemies as BattleActor[] ?? enemies.ToArray();
            
            foreach (var enemy in battleActors)
            {
                enemy.Components.Get<ActorSelectComponent>().ActorSelected +=(x) =>
                {
                    _model.SelectActor(x);
                    battleActors.ForEach(battleActor => battleActor.Components.Get<ActorSelectComponent>().Clear());
                };
            }
        }

        private void HandleSkillSelected(int skillIndex)
        {
            _model.Cancel();
            
            Debug.Log($"Skill {skillIndex}!");
            CompleteTurn();
        }

        private void HandleItemSelected(int itemIndex)
        {
            Debug.Log($"Item {itemIndex}!");
            CompleteTurn();
        }
        
        private void HandleGuardSelected()
        {
            Debug.Log("Guard!");
            CompleteTurn();
        }

        protected override void Dispose()
        {
            _battleHUDController.AttackSelected -= HandleAttackSelected;
            _battleHUDController.SkillSelected -= HandleSkillSelected;
            _battleHUDController.ItemSelected -= HandleItemSelected;
            _battleHUDController.GuardSelected -= HandleGuardSelected;
            _battleHUDController.HideHUD();
        }
    }
}