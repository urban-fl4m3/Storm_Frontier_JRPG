using SF.Battle.Abilities.Mechanics.Data;
using SF.Battle.Actors;
using SF.Battle.Damage;
using SF.Common.Actors;

namespace SF.Battle.Abilities.Mechanics.Logic
{
    public class HealMechanicLogic: BaseMechanicLogic<HealMechanicData>
    {
        public override void Invoke(BattleActor caster, IActor selected)
        {
            var targets = GetMechanicTargets(caster, selected);
            var healAmount = Data.Amount;

            foreach (var target in targets)
            {
                var damageable = target.Components.Get<IHealthChangeable>();
                damageable.TakeHeal(healAmount);
            }
        }

        protected override void OnDataSet(HealMechanicData data)
        {
        }
    }
}