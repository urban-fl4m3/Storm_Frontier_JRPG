using SF.Battle.Damage;
using SF.Common.Actors;
using SF.Common.Actors.Components.Stats;
using SF.Game.Stats;

namespace SF.Battle.Actions
{
    public class DamageAction : BattleAction
    {
        private readonly IActor _actor;
        private readonly IDamageProvider _damageProvider;
        private readonly StatsContainerComponent _statsContainer;

        private IActor _target;
        
        public DamageAction(IActor actor)
        {
            _actor = actor;
            _damageProvider = actor.Components.Get<IDamageProvider>();
            _statsContainer = actor.Components.Get<StatsContainerComponent>();
        }

        public void SetTarget(IActor target)
        {
            _target = target;
        }
        
        public override void Execute()
        {
            var damageTaker = _target.Components.Get<IDamageable>();
            var attackDamage = _statsContainer.GetStat(PrimaryStat.PPower);
            damageTaker?.TakeDamage(_actor, _damageProvider, new DamageMeta(attackDamage));
        }
    }
}