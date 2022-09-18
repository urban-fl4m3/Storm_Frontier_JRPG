using System.Collections.Generic;
using SF.Battle.Common;
using SF.Common.Logger;
using SF.Game;
using SF.Game.Extensions;

namespace SF.Battle.Actors
{
    public class BattleActorsHolder : IBattleActorsHolder
    {
        private readonly IDebugLogger _logger;
        
        private readonly Dictionary<Team, List<BattleActor>> _teams = new();
        private readonly List<BattleActor> _actingActors = new();

        public BattleActorsHolder(IDebugLogger logger)
        {
            _logger = logger;
        }

        public IEnumerable<BattleActor> GetAllActors()
        {
            return _actingActors;
        }

        public IEnumerable<BattleActor> GetTeamActors(Team team)
        {
            if (!_teams.ContainsKey(team))
            {
                _logger.LogError($"Team {team} doesn't exists in actor registerer");
                return null;
            }

            return _teams[team];
        }

        public IEnumerable<BattleActor> GetOppositeTeamActors(Team team)
        {
            var enemyTeam = team.GetOppositeTeam();

            return GetTeamActors(enemyTeam);
        }

        public void AddActor(BattleActor actor, Team team)
        {
            if (!_teams.ContainsKey(team))
            {
                _teams.Add(team, new List<BattleActor>());
            }

            _teams[team].Add(actor);
            _actingActors.Add(actor);
        }
    }
}