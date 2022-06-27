using System;
using System.Linq;
using SF.Battle.Abilities;
using SF.Battle.Actors;
using SF.Common.Actors.Abilities;
using SF.Game;
using SF.UI.Creator;
using SF.UI.View;

namespace SF.UI.Controller
{
    public class PlayerActionsController : BattleWorldUiController
    {
        public event Action GuardSelected = delegate { };
        public event Action AttackSelected = delegate { };
        public event Action<int> ItemSelected = delegate { };
        public event Action<string> SkillSelected = delegate { };

        private readonly PlayerActionButtonsView _view;

        private ButtonCreator _buttonCreator;
        private BattleActor _currentActor;
        private IDisposable _activeActorObserver;

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

            _buttonCreator = new ButtonCreator(_view.PanelView.Content, _view.PanelView.ButtonView);
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
            _buttonCreator.Clear();

            var abilities = _currentActor.MetaData.Info.Config.Abilities.Where(a => !a.IsPassive).ToList();

            for (var i = 0; i < abilities.Count(); i++)
            {
                var ability = abilities[i];
                var button = _buttonCreator.Get();

                button.SetAbilityName(ability.Name);
                button.AddActionOnClick(() => SkillSelected.Invoke(ability.Name));
            }
        }
    }
}