using System;
using SF.Battle.Actions;
using SF.Battle.Damage;
using SF.Common.Actors.Actions;

namespace SF.Common.Actors.Weapon
{
    public class WeaponComponent : ActorComponent, IDamageProvider
    {
        private ActionControllerComponent _actionControllerComponent;
        private DamageAction _damageAction;
        
        public void InvokeAttack(IActor target, Action onActionComplete = null)
        {
            _damageAction.SetTarget(target);
            _actionControllerComponent.MakeAction(_damageAction, onActionComplete);
        }

        protected override void OnInit()
        {
            _actionControllerComponent = Owner.Components.Get<ActionControllerComponent>();
            _damageAction = new DamageAction(Owner);
            
            base.OnInit();
        }
    }
}