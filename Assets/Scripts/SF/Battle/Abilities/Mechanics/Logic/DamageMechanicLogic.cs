﻿using SF.Battle.Abilities.Mechanics.Data;
using SF.Battle.Actors;
using SF.Battle.Damage;
using SF.Common.Actors;

namespace SF.Battle.Abilities.Mechanics.Logic
{
    public class DamageMechanicLogic : BaseMechanicLogic<DamageMechanicData>
    {
        public override void Invoke(BattleActor caster, IActor selectedActor)
        {
            var targets = GetMechanicTargets(caster, selectedActor);

            var casterDamageProvider = caster.Components.Get<IDamageProvider>();
            var damageMeta = new DamageMeta((int) Data.Amount);
            
            foreach (var target in targets)
            {
                if (target is BattleActor battleActor)
                {
                    battleActor.TakeDamage(caster, casterDamageProvider, damageMeta);
                }
            }
        }

        protected override void OnDataSet(DamageMechanicData data)
        {
            
        }
    }
}