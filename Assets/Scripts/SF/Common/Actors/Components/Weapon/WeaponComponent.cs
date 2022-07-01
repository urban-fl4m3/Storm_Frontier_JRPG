using System;
using SF.Battle.Actions;
using SF.Battle.Damage;
using SF.Common.Actors.Actions;
using SF.Common.Actors.Components.Stats;
using SF.Game.Stats;

namespace SF.Common.Actors.Weapon
{
    public class WeaponComponent : ActorComponent, IDamageProvider
    {
        private ActionControllerComponent _actionControllerComponent;
        private StatsContainerComponent _statsContainerComponent;
        private DamageAction _damageAction;
        
        public void InvokeAttack(IActor target, Action onActionComplete = null)
        {
            _damageAction.SetTarget(target);
            _actionControllerComponent.MakeAction(_damageAction, onActionComplete);
        }

        public int GetDamage()
        {
            const bool isMagicWeapon = false;
            const int weaponMight = 150;

            var damageType = PrimaryStat.PPower;
            
            if (isMagicWeapon)
            {
                damageType = PrimaryStat.MPower;
            }

            var might = _statsContainerComponent.GetStat(damageType);
            return might + weaponMight;
        }

        protected override void OnInit()
        {
            _actionControllerComponent = Owner.Components.Get<ActionControllerComponent>();
            _statsContainerComponent = Owner.Components.Get<StatsContainerComponent>();
            
            _damageAction = new DamageAction(this);
            
            base.OnInit();
        }
    }
}