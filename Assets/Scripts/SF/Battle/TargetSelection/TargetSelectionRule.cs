using System;
using System.Collections.Generic;
using System.Linq;
using SF.Battle.Actors;

namespace SF.Battle.TargetSelection
{
    public class TargetSelectionRule : ITargetSelectionRule
    {
        private readonly BattleActor _actingActor;
        private readonly TargetSelectionData _data;
        
        public TargetSelectionRule(BattleActor actingActor, TargetSelectionData data)
        {
            _data = data;
            _actingActor = actingActor;
        }

        public BattleActor[] GetPossibleTargets(IEnumerable<BattleActor> actors)
        {
            var expectedPick = _data.Pick;

            if (expectedPick == TargetPick.Instant)
            {
                return Array.Empty<BattleActor>();
            }

            var expectedActors = actors as BattleActor[] ?? actors.ToArray();

            expectedActors = expectedPick switch
            {
                TargetPick.AllyTeam => expectedActors.Where(x => x.Team == _actingActor.Team).ToArray(),
                TargetPick.OppositeTeam => expectedActors.Where(x => x.Team != _actingActor.Team).ToArray(),
                _ => expectedActors
            };

            return expectedActors;
        }
    }
}