using System.Collections.Generic;
using SF.Battle.Abilities.Mechanics.Logic;
using SF.Battle.Actors;
using SF.Battle.TargetSelection;
using SF.Common.Actors;
using Sirenix.Utilities;

namespace SF.Battle.Abilities
{
    public class BattleAbility
    {
        public TargetPick Pick { get; }

        private readonly BattleActor _caster;
        private readonly IEnumerable<IMechanicLogic> _mechanicLogics;
        
        public BattleAbility(BattleActor caster, IEnumerable<IMechanicLogic> mechanics, TargetPick pick)
        {
            Pick = pick;
            
            _caster = caster;
            _mechanicLogics = mechanics;
        }

        public void InvokeAbility(IActor targetSelected)
        {
            _mechanicLogics.ForEach(mechanicLogic => mechanicLogic.Invoke(_caster, targetSelected));
        }
    }
}