using System;
using SF.Battle.Stats;
using SF.Common.Actors.Components.Animations;
using SF.Common.Actors.Components.Stats;
using SF.Game.Stats;
using UniRx;

namespace SF.Common.Actors.Components.Status
{
    public class ActorStateComponent: ActorComponent
    {
        public IReadOnlyReactiveProperty<ActorState> State => _state;

        //todo refactor later, to remove
        private readonly ReactiveProperty<ActorState> _state = new(ActorState.Standard);
        
        private BattleAnimationComponent _animatorController;
        private IPrimaryStatResource _healthResources;
        private IDisposable _healthChangeSub;

        protected override void OnInit()
        {
            var statHolder = Owner.Components.Get<IStatHolder>();
            var statContainer = statHolder.GetStatContainer();
            
            _healthResources = statContainer.GetStatResourceResolver(PrimaryStat.HP);
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
            if (_healthResources != null && _healthChangeSub == null)
            {
                _healthChangeSub = _healthResources.Current
                    .SkipLatestValueOnSubscribe()
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