﻿using System;
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
        private readonly ButtonsHolder<TextButtonView> _buttonsHolder;
        
        public AbilityPanelPresenter(
            AbilityPanelView view,
            IWorld world,
            IServiceLocator serviceLocator,
            ActionBinder actionBinder)
            : base(view, world, serviceLocator, actionBinder)
        {
            _buttonsHolder = new ButtonsHolder<TextButtonView>(View.Content, View.ButtonView);
        }

        public override void Enable()
        {
            View.Show();
        }

        public override void Disable()
        {
            _buttonsHolder.Clear();
            
            View.Hide();
        }
        
        public void SubscribeOnAbilities(BattleActor actor, Action<ActiveBattleAbilityData> skillSelected)
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
                    skillSelected.Invoke(abilityData);
                });
            }
        }
    }
}