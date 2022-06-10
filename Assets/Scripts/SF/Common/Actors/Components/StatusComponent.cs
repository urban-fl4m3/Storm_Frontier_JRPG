using System;
using SF.Common.Actors.Components.Animations;
using SF.Common.Actors.Components.Stats;
using UnityEngine;
using UniRx;

namespace SF.Common.Actors
{
    public class StatusComponent: ActorComponent
    {
        [SerializeField] private HealthComponent _healthComponent;
        [SerializeField] private BattleAnimationComponent _animatorController;

        public ActorStatus Status { get; private set; } = ActorStatus.Standard;
        
        private IDisposable _healthChangeSub;

        private void OnEnable()
        {
            _healthChangeSub = _healthComponent.CurrentHealth.Subscribe(CheckDeadStatus);
        }

        private void OnDisable()
        {
            _healthChangeSub.Dispose();
            _healthChangeSub = null;
        }

        private void CheckDeadStatus(int amount)
        {
            if (amount != 0) return;
            
            Status = ActorStatus.Dead;
            _animatorController.SetDeadTrigger();
        }
    }
}