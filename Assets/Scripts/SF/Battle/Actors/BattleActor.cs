using SF.Battle.Common;
using SF.Common.Actors;
using SF.Game;

namespace SF.Battle.Actors
{
    public class BattleActor : Actor
    {
        public Team Team { get; private set; }
        public int Level { get; private set; }
        
        public void Init(IServiceLocator serviceLocator, BattleMetaData metaData)
        {
            Init(serviceLocator);

            Team = metaData.Team;
            Level = metaData.Level;
        }
    }
}