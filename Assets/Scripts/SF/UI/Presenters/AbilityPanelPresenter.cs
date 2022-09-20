using System;
using SF.Battle.Abilities;
using SF.Battle.Actors;
using SF.Common.Actors.Abilities;
using SF.Game;
using SF.Game.Worlds;
using SF.UI.Creator;
using SF.UI.Models.Actions;
using SF.UI.View;

namespace SF.UI.Presenters
{
    public class AbilityPanelPresenter : BaseBattlePresenter<AbilityPanelView>
    {
        private readonly Action<ActiveBattleAbilityData> _skillSelected;
        private readonly ButtonsHolder<TextButtonView> _buttonsHolder;
        
        public AbilityPanelPresenter(
            AbilityPanelView view,
            Action<ActiveBattleAbilityData> skillSelected,
            IWorld world,
            IServiceLocator serviceLocator,
            ActionBinder actionBinder)
            : base(view, world, serviceLocator, actionBinder)
        {
            _skillSelected = skillSelected;
            _buttonsHolder = new ButtonsHolder<TextButtonView>(view.Content, view.ButtonView);
        }

        public override void Enable()
        {
            var actingActor = World.Turns.GetActingActor();

            if (!actingActor)
            {
                ServiceLocator.Logger.LogError($"Can't show abilities for null actor");
                return;
            }

            View.Show();
            View.SkillDescriptionView.Show();
            
            SubscribeOnAbilities(actingActor);
        }

        public override void Disable()
        {
            _buttonsHolder.Clear();
            
            View.Hide();
            View.SkillDescriptionView.Hide();
        }

        private void SubscribeOnAbilities(BattleActor actor)
        {
            var abilities = actor.MetaData.Info.Config.Abilities;
            var abilityComponent = actor.Components.Get<AbilityComponent>();
            
            foreach (var abilityData in abilities)
            {
                var button = _buttonsHolder.Get();

                if (!abilityComponent.CanInvoke(abilityData))
                {
                    button.ChangeInteractable(false);
                    
                    continue;
                }
                
                button.ChangeInteractable(true);
                button.SetText(abilityData.Name);
                button.AddActionOnClick(() =>
                {
                    View.Hide();
                    _skillSelected(abilityData);
                });
            }
        }
    }
}