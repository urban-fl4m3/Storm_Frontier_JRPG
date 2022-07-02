using SF.Battle.Abilities.Mechanics.Data;
using SF.Battle.Actors;
using SF.Common.Actors;
using SF.Common.Actors.Effects;

namespace SF.Battle.Abilities.Mechanics.Logic
{
    public class EffectMechanicLogic : BaseMechanicLogic<EffectMechanicData>
    {
        public override void Invoke(BattleActor caster, IActor selected)
        {
            var targets = GetMechanicTargets(caster, selected);

            foreach (var target in targets)
            {
                var damageable = target.Components.Get<EffectsComponent>();
                damageable.Apply(Data, caster);
            }
        }

        protected override void OnDataSet(EffectMechanicData data)
        {
            
        }
    }
}