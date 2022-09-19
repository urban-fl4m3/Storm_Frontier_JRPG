using System.Collections.Generic;
using SF.Battle.Actors;
using SF.Battle.Stats;
using SF.Common.Actors.Components.Stats;
using SF.Game;
using SF.Game.Stats;
using SF.Game.Worlds;
using SF.UI.Models.Actions;
using SF.UI.View;

namespace SF.UI.Presenters
{
    public class CharacterInfoPresenter : BaseBattlePresenter<CharacterInfoView>
    {
        private readonly BattleActor _actor;
        private readonly List<CharacterStatBarPresenter> _statBarPresenters = new();

        public CharacterInfoPresenter(
            CharacterInfoView view,
            BattleActor actor,
            IWorld world,
            IServiceLocator serviceLocator,
            ActionBinder actionBinder)
            : base(view, world, serviceLocator, actionBinder)
        {
            _actor = actor;
        }

        public override void Enable()
        {
            ObserveActorResources();
            
            View.Show();
        }

        public override void Disable()
        {
            foreach (var statBarPresenter in _statBarPresenters)
            {
                statBarPresenter.Disable();
            }
            
            View.Hide();
        }
        
        private void ObserveActorResources()
        {
            var visualParameter = _actor.MetaData.Info.Config;
            
            View.SetIcon(visualParameter.Icon);

            var statHolder = _actor.Components.Get<IStatHolder>();
            var statContainer = statHolder.GetStatContainer();
            
            var healthResolver = statContainer.GetStatResourceResolver(PrimaryStat.HP);
            var manaResolver = statContainer.GetStatResourceResolver(PrimaryStat.MP);

            TryObserveResource(healthResolver, View.HealthStatBarView);
            TryObserveResource(manaResolver, View.ManaStatBarView);
        }

        private void TryObserveResource(IPrimaryStatResource statResolver, CharacterStatBarView view)
        {
            if (view == null) return;
            
            var statBarPresenter = new CharacterStatBarPresenter(view, statResolver, World, ServiceLocator, ActionBinder);
            statBarPresenter.Enable();
                
            _statBarPresenters.Add(statBarPresenter);
        }
    }
}