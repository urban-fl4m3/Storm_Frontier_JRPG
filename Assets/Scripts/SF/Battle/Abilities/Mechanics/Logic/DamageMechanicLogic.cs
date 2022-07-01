﻿using SF.Battle.Abilities.Mechanics.Data;
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
            
            var casterDamageProvider = caster.Components.Get<IHPChangeProvider>();
            var damageMeta = new HPChangeMeta((int) Data.Amount);

            foreach (var target in targets)
            {
                var damageable = target.Components.Get<IHPChangeable>();
                damageable.TakeDamage(caster, casterDamageProvider, damageMeta);
            }
        }

        protected override void OnDataSet(DamageMechanicData data)
        {
            
        }
    }
}