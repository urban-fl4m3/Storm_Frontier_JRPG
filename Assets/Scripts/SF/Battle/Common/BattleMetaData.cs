using SF.Battle.Data;
using SF.Game;

namespace SF.Battle.Common
{
    public struct BattleMetaData
    {
        public Team Team { get; }
        public BattleCharacterInfo Info { get; }

        public BattleMetaData(Team team, BattleCharacterInfo info)
        {
            Team = team;
            Info = info;
        }
    }
}