using System.Collections.Generic;
using SF.Battle.Common;
using SF.Common.Logger;
using SF.Game;
using SF.Game.Extensions;

namespace SF.Battle.Actors
{
    public class BattleActorsHolder : IBattleActorsHolder
    {
        public BattleActor ActingActor { get; private set; }
        public IEnumerable<BattleActor> Actors => _actingActors;
     
        private readonly IDebugLogger _logger;
        
        private readonly Dictionary<Team, HashSet<BattleActor>> _teams = new();
        private readonly List<BattleActor> _actingActors = new();

        public BattleActorsHolder(IDebugLogger logger)
        {
            _logger = logger;
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
                _teams.Add(team, new HashSet<BattleActor>());
            }

            _teams[team].Add(actor);
            _actingActors.Add(actor);
        }
    }
}