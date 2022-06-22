using System;
using System.Linq;
using SF.Battle.Abilities;
using SF.Battle.Actors;
using SF.Game;
using SF.UI.View;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SF.UI.Controller
{
    public class PlayerActionsController : BattleWorldUiController
    {
        public event Action GuardSelected = delegate {  };
        public event Action AttackSelected = delegate {  };
        public event Action<int> ItemSelected = delegate {  };
        public event Action<BattleAbilityData> SkillSelected = delegate {  };

        private readonly PlayerActionButtonsView _view;

        private IDisposable _activeActorObserver;
        private BattleActor _currentActor;

        public PlayerActionsController(PlayerActionButtonsView view, IWorld world, IServiceLocator serviceLocator)
            : base(world, serviceLocator)
        {
            _view = view;
        }

        public void Init()
        {
            _view.AttackButton.onClick.AddListener(OnAttackClick);
            _view.SkillButton.onClick.AddListener(OnSkillClick);
            _view.UseItemButton.onClick.AddListener(OnItemClick);
            _view.GuardButton.onClick.AddListener(OnGuardClick);
        }

        public void ShowView() => _view.Show();

        public void HideView() => _view.Hide();
        public void SetCurrentActor(BattleActor actor) => _currentActor = actor;

        private void OnAttackClick()
        {
            _view.HideAbility();
            AttackSelected?.Invoke();
        }

        private void OnSkillClick()
        {
            _view.ShowAbility();
            CreateAbilityList();
        }

        private void OnItemClick()
        {
            _view.HideAbility();
            ItemSelected?.Invoke(0);
        }

        private void OnGuardClick()
        {
            _view.HideAbility();
            GuardSelected?.Invoke();
        }

        private void CreateAbilityList()
        {
            var content = _view.PanelView.Content;
            var prefab = _view.PanelView.ButtonView;
            var abilities = _currentActor.MetaData.Info.Config.Abilities.Where(a => !a.IsPassive).ToList();
            var childCount = content.transform.childCount;
            var lastIndex = 0;

            for (var i = 0; i < abilities.Count(); i++)
            {
                var ability = abilities[i];
                var button = childCount <= i
                    ? Object.Instantiate(prefab, content)
                    : content.GetChild(i).GetComponent<AbilityButtonView>();

                button.Clear();
                button.gameObject.SetActive(true);
                button.SetAbilityName(ability.Name);
                button.AddActionOnClick(() => SkillSelected.Invoke(ability));
                
                lastIndex = i;
            }

            for (var i = lastIndex; i < childCount; i++)
            {
                content.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}