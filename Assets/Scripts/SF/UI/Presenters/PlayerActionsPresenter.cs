using System;
using SF.Battle.Abilities;
using SF.Battle.Actors;
using SF.Battle.Turns;
using SF.Common.Data;
using SF.Game;
using SF.Game.Worlds;
using SF.UI.Models.Actions;
using SF.UI.View;

namespace SF.UI.Presenters
{
    public class PlayerActionsPresenter : BaseBattlePresenter<PlayerActionButtonsView>
    {
        private readonly AbilityPanelPresenter _abilityPanelPresenter;
        
        private BattleActor _currentActor;
        private IDisposable _activeActorObserver;

        public PlayerActionsPresenter(
            PlayerActionButtonsView view,
            IWorld world,
            IServiceLocator serviceLocator,
            ActionBinder actionBinder)
            : base(view, world, serviceLocator, actionBinder)
        {
            _abilityPanelPresenter =
                new AbilityPanelPresenter(view.AbilityPanelView, OnSkillSelected, world, serviceLocator, actionBinder);
        }

        public override void Enable()
        {
            View.AttackButton.onClick.AddListener(OnAttackClick);
            View.SkillButton.onClick.AddListener(OnSkillClick);
            View.UseItemButton.onClick.AddListener(OnItemClick);
            View.GuardButton.onClick.AddListener(OnGuardClick);
            
            View.Hide();
            _abilityPanelPresenter.Disable();
            
            World.Turns.TurnStarted += HandleTurnStarted;
        }

        public override void Disable()
        {
            View.AttackButton.onClick.RemoveListener(OnAttackClick);
            View.SkillButton.onClick.RemoveListener(OnSkillClick);
            View.UseItemButton.onClick.RemoveListener(OnItemClick);
            View.GuardButton.onClick.RemoveListener(OnGuardClick);
            
            View.Hide();
            
            World.Turns.TurnStarted -= HandleTurnStarted;
            World.Turns.TurnCompleted -= HandleTurnCompleted;
        }

        private void HandleTurnStarted(ITurnAction turn)
        {
            var isSelectionStep = turn.Phase == ActPhase.Wait;
            var isPlayerSide = turn.ActingActor.Team == Team.Player;
            
            if (isSelectionStep && isPlayerSide)
            {
                View.Show();
                
                World.Turns.TurnCompleted += HandleTurnCompleted;
            }
        }

        private void HandleTurnCompleted(ITurnAction turn)
        {
            World.Turns.TurnCompleted -= HandleTurnCompleted;
            
            View.Hide();
        }

        private void OnAttackClick()
        {
            _abilityPanelPresenter.Disable();
            
            RaiseAction(ActionName.Attack);
        }

        private void OnSkillClick()
        {
            _abilityPanelPresenter.Enable();
        }

        private void OnSkillSelected(ActiveBattleAbilityData data)
        {
            RaiseAction(ActionName.Skills, new DataProvider(data));
        }

        private void OnItemClick()
        {
            _abilityPanelPresenter.Disable();
            
            RaiseAction(ActionName.Item, new DataProvider(0));
        }

        private void OnGuardClick()
        {
            _abilityPanelPresenter.Disable();
            
            RaiseAction(ActionName.Guard);
        }
    }
}