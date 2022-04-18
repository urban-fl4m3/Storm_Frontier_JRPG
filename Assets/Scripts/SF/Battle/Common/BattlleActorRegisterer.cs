using System.Collections.Generic;
using SF.Battle.Actors;
using SF.Common.Actors;
using SF.Common.Logger;
using SF.Game;
using SF.Game.Extensions;

namespace SF.Battle.Common
{
    public class BattlleActorRegisterer : ActorRegisterer<BattleActor>
    {
        private readonly Dictionary<Team, HashSet<BattleActor>> _teams = new Dictionary<Team, HashSet<BattleActor>>();
        
        public BattlleActorRegisterer(IDebugLogger logger) : base(logger)
        {
        }

        public bool AddToTeam(BattleActor actor, Team team)
        {
            var isAdded = Add(actor);

            if (!isAdded) return false;
            
            if (!_teams.ContainsKey(team))
            {
                _teams.Add(team, new HashSet<BattleActor>());
            }

            _teams[team].Add(actor);
            
            return true;
        }

        public bool RemoveFromTeam(BattleActor actor, Team team)
        {
            var isRemoved = Remove(actor);

            if (!isRemoved) return false;

            if (!_teams.ContainsKey(team))
            {
                Logger.LogWarning($"Cannot remove {actor} from {team} team.");
                return false;
            }

            _teams[team].Remove(actor);

            return true;
        }

        public IEnumerable<BattleActor> GetTeamActors(Team team)
        {
            if (!_teams.ContainsKey(team))
            {
                Logger.LogError($"Team {team} doesn't exists in actor registerer");
                return null;
            }

            return _teams[team];
        }

        public IEnumerable<BattleActor> GetEnemyTeamActors(Team team)
        {
            var enemyTeam = team.GetOppositeTeam();

            return GetTeamActors(enemyTeam);
        }
    }
}