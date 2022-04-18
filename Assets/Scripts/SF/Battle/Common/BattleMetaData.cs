using SF.Game;

namespace SF.Battle.Common
{
    public struct BattleMetaData
    {
        public Team Team { get; }
        public int Level { get; }

        public BattleMetaData(Team team, int level)
        {
            Team = team;
            Level = level;
        }
    }
}