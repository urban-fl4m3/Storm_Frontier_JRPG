using System;
using SF.Battle.Actions;
using SF.Battle.Damage;
using SF.Battle.Stats;
using SF.Common.Actors.Actions;
using SF.Game.Stats;

namespace SF.Common.Actors.Weapon
{
    public class WeaponComponent : ActorComponent, IDamageProvider
    {
        private ActionControllerComponent _actionControllerComponent;
        private StatContainer _statContainer;
        private DamageAction _damageAction;
        
        public void InvokeAttack(IActor target, Action onActionComplete = null)
        {
            _damageAction.SetTarget(target);
            _actionControllerComponent.MakeAction(_damageAction, onActionComplete);
        }

        public int GetDamage()
        {
            // const bool isMagicWeapon = false;
            const int weaponMight = 50;

            var damageType = PrimaryStat.PPower;
            
            // if (isMagicWeapon)
            // {
            //     damageType = PrimaryStat.MPower;
            // }

            var might = _statContainer.GetStat(damageType);
            return might + weaponMight;
        }

        protected override void OnInit()
        {
            _actionControllerComponent = Owner.Components.Get<ActionControllerComponent>();
            _statContainer = Owner.Components.Get<IStatHolder>().GetStatContainer();
            
            _damageAction = new DamageAction(this);
            
            base.OnInit();
        }
    }
}