using SF.Battle.Abilities.Mechanics.Data;
using SF.Battle.Actors;
using SF.Battle.Damage;
using SF.Common.Actors;

namespace SF.Battle.Abilities.Mechanics.Logic
{
    public class DamageMechanicLogic : BaseMechanicLogic<DamageMechanicData>
    {
        public override void Invoke(BattleActor caster, IActor selected)
        {
            var targets = GetMechanicTargets(caster, selected);
            
            var casterDamageProvider = caster.Components.Get<IDamageProvider>();
            var damage = casterDamageProvider.GetDamage();

            if (Data.IsFlat)
            {
                damage = (int)Data.Amount;
            }
            else
            {
                damage = (int)(Data.Amount * damage);
            }
            
            var damageMeta = new DamageMeta(damage);

            foreach (var target in targets)
            {
                var damageable = target.Components.Get<IHealthChangeable>();
                damageable.TakeDamage(damageMeta);
            }
        }

        protected override void OnDataSet(DamageMechanicData data)
        {
            
        }
    }
}