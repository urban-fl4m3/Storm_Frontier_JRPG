using SF.Battle.Abilities.Mechanics.Data;
using SF.Common.Actors.Components.Stats;

namespace SF.Battle.Effects
{
    public class StatEffect : BaseEffect<StatEffectMechanicData>
    {
        protected override void OnApply()
        {
            var affectedStats = Affected.Components.Get<StatsContainerComponent>();
            affectedStats.AddStatValue(Data.Stat, (int)Data.StatBoostValue);
        }

        protected override void OnCancel()
        {
            var affectedStats = Affected.Components.Get<StatsContainerComponent>();
            affectedStats.AddStatValue(Data.Stat, (int)Data.StatBoostValue * -1);
        }
    }
}