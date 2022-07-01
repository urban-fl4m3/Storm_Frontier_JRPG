using SF.Battle.Damage;
using SF.Common.Actors;

namespace SF.Battle.Actions
{
    public class DamageAction : BattleAction
    {
        private readonly IDamageProvider _damageProvider;

        private IActor _target;
        
        public DamageAction(IDamageProvider damageProvider)
        {
            _damageProvider = damageProvider;
        }

        public void SetTarget(IActor target)
        {
            _target = target;
        }
        
        public override void Execute()
        {
            var damageTaker = _target.Components.Get<IHealthChangeable>();
            var attackDamage = _damageProvider.GetDamage();
            
            damageTaker?.TakeDamage(new DamageMeta(attackDamage));
        }
    }
}