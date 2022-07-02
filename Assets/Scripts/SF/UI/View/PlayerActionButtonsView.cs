using System;
using SF.Battle.Abilities;
using SF.Battle.Actors;
using SF.Common.Actors.Abilities;
using SF.UI.Creator;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace SF.UI.View
{
    public class PlayerActionButtonsView : SerializedMonoBehaviour, IView
    {
        [SerializeField] private Button _attackButton;
        [SerializeField] private Button _skillButton;
        [SerializeField] private Button _useItemButton;
        [SerializeField] private Button _guardButton;
        [SerializeField] private AbilityPanelView _abilityPanelView;
        
        public Button AttackButton => _attackButton;
        public Button SkillButton => _skillButton;
        public Button UseItemButton => _useItemButton;
        public Button GuardButton => _guardButton;
        
        private ButtonsHolder<TextButtonView> _buttonsHolder;
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void ShowAbility()
        {
            _abilityPanelView.Root.gameObject.SetActive(true);
        }
        
        public void HideAbility()
        {
            _buttonsHolder.Clear();
            _abilityPanelView.Root.gameObject.SetActive(false);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
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
                    HideAbility();
                    skillSelected.Invoke(abilityData);
                });
            }
        }
        
        private void Start()
        {
            _buttonsHolder = new ButtonsHolder<TextButtonView>(_abilityPanelView.Content, _abilityPanelView.ButtonView);   
        }
    }
}