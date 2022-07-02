using System;
using SF.Common.Actors.Components.Animations;
using SF.Common.Actors.Components.Stats;
using UniRx;

namespace SF.Common.Actors.Components.Status
{
    public class ActorStateComponent: ActorComponent
    {
        public IReadOnlyReactiveProperty<ActorState> State => _state;

        private readonly ReactiveProperty<ActorState> _state = new ReactiveProperty<ActorState>(ActorState.Standard);
        
        private BattleAnimationComponent _animatorController;
        private HealthComponent _healthComponent;
        private IDisposable _healthChangeSub;

        protected override void OnInit()
        {
            _healthComponent = Owner.Components.Get<HealthComponent>();
            _animatorController = Owner.Components.Get<BattleAnimationComponent>();
            
            ObserveCurrentHealth();
            
            base.OnInit();
        }

        private void OnEnable()
        {
            ObserveCurrentHealth();
        }

        private void OnDisable()
        {
            _healthChangeSub?.Dispose();
            _healthChangeSub = null;
        }

        private void ObserveCurrentHealth()
        {
            if (_healthComponent != null && _healthChangeSub == null)
            {
                _healthChangeSub = _healthComponent.Current.SkipLatestValueOnSubscribe()
                    .Subscribe(CheckDeadStatus);
            }
        }

        private void CheckDeadStatus(int amount)
        {
            if (amount != 0) return;
            
            _state.Value = ActorState.Dead;
            _animatorController.SetDeadTrigger();
        }
    }
}