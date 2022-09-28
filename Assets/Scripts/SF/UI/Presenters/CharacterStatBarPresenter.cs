using System;
using SF.Common.Actors.Components.Stats;
using SF.Game;
using SF.Game.Worlds;
using SF.UI.Models.Actions;
using SF.UI.View;
using UniRx;

namespace SF.UI.Presenters
{
    public class CharacterStatBarPresenter : BaseBattlePresenter<CharacterStatBarView>
    {
        private readonly IPrimaryStatResource _resourceResolver;
        
        private IDisposable _statChangeSub;

        public CharacterStatBarPresenter(
            CharacterStatBarView view,
            IPrimaryStatResource resourceResolver,
            IWorld world,
            IServiceLocator serviceLocator,
            ActionBinder actionBinder) 
            : base(view, world, serviceLocator, actionBinder)
        {
            _resourceResolver = resourceResolver;
        }

        public override void Enable()
        {
            StartObserve();
            
            View.Hide();
        }

        public override void Disable()
        {
            _statChangeSub?.Dispose();
            _statChangeSub = null;
            
            View.Show();
        }
        
        private void StartObserve()
        {
            _statChangeSub ??= _resourceResolver
                .Current
                .Subscribe(HandleStatChange);
        }
        
        private void HandleStatChange(int amount)
        {
            var maxValue = _resourceResolver.Max.Value;
            
            View.ChangeStatText(amount);
            View.ChangeStatFill(amount, maxValue);
        }
    }
}