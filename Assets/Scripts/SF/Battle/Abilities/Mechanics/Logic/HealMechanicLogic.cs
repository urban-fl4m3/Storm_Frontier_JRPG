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

            var casterHealProvider = caster.Components.Get<IHPChangeProvider>();
            var healMeta = new HPChangeMeta((int)Data.HealAmount);

            foreach (var target in targets)
            {
                var damageable = target.Components.Get<IHPChangeable>();
                damageable.TakeHeal(caster, casterHealProvider, healMeta);
            }
        }

        protected override void OnDataSet(HealMechanicData data)
        {
        }
    }
}