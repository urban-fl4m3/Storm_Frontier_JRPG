using SF.Battle.Turns;
using SF.Game.Stats;

namespace SF.Game.Extensions
{
    public static class ActPhaseExtensions
    {
        public static PrimaryStat GetPhaseFillStat(this ActPhase phase)
        {
            return phase == ActPhase.Wait ? PrimaryStat.ActSpeed : PrimaryStat.CastSpeed;
        }
    }
}