using SF.Game.Stats;

namespace SF.Common.Actors.Components.Stats
{
    public class ManaComponent : BaseResourceStatComponent
    {
        protected override int Min => 0;
        protected override PrimaryStat Stat => PrimaryStat.MP;
    }
}