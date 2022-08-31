using SF.Battle.Abilities.Mechanics.Data;
using SF.Battle.Stats;

namespace SF.Battle.Effects
{
    public class StatEffect : BaseEffect<StatEffectMechanicData>
    {
        protected override void OnApply()
        {
            var affectedStats = GetStatContainer();
            affectedStats.AddStatValue(Data.Stat, (int)Data.StatBoostValue);
        }

        protected override void OnCancel()
        {
            var affectedStats = GetStatContainer();
            affectedStats.AddStatValue(Data.Stat, (int)Data.StatBoostValue * -1);
        }

        private StatContainer GetStatContainer()
        {
            var statHolder = Affected.Components.Get<IStatHolder>();
            return statHolder.GetStatContainer();
        }
    }
}