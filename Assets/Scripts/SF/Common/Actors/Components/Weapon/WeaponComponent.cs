using System;
using SF.Battle.Damage;
using SF.Common.Actors.Components.Animations;
using SF.Common.Actors.Components.Stats;
using SF.Common.Animations;
using SF.Game.Stats;

namespace SF.Common.Actors.Weapon
{
    public class WeaponComponent : ActorComponent, IDamageProvider
    {
        private BattleAnimationComponent _battleAnimationComponent;
        private AnimationEventHandler _animationEventHandler;
        private StatsContainerComponent _statsContainer;

        //We should pass IAction as a parameter. Like AttackAction, SkillAction, ItemAction etc and call not DoStuff,
        //but IAction.DoAction
        public void MakeAction(IActor target, Action onActionComplete = null)
        {
            var damageTaker = target.Components.Get<IDamageable>();
            
            _animationEventHandler.Subscribe("ActionEvent", DoStuff);
            
            _battleAnimationComponent.ActionEnds += HandleActionComplete;
            _battleAnimationComponent.SetAttackTrigger();

            void HandleActionComplete()
            {
                _battleAnimationComponent.ActionEnds -= HandleActionComplete;
                _animationEventHandler.Unsubscribe("ActionEvent", DoStuff);
                
                onActionComplete?.Invoke();
            }

            void DoStuff(object sender, EventArgs e)
            {
                var attackDamage = _statsContainer.GetStat(PrimaryStat.PPower);
                damageTaker?.TakeDamage(Owner, this, new DamageMeta(attackDamage));
            }
        }
        
        protected override void OnInit()
        {
            _battleAnimationComponent = Owner.Components.Get<BattleAnimationComponent>();
            _animationEventHandler = Owner.Components.Get<AnimationEventHandler>();
            _statsContainer = Owner.Components.Get<StatsContainerComponent>();
            
            base.OnInit();
        }
    }
}