using System.Collections.Generic;
using SF.Common.Actors;
using SF.Battle.Abilities.Mechanics.Logic;
using SF.Battle.TargetSelection;
using Sirenix.Utilities;

namespace SF.Battle.Abilities
{
    public class BattleAbility
    {
        public TargetPick Pick { get; }

        private readonly IActor _caster;
        private readonly IEnumerable<IMechanicLogic> _mechanicLogics;
        
        public BattleAbility(IActor caster, IEnumerable<IMechanicLogic> mechanics, TargetPick pick)
        {
            Pick = pick;
            
            _caster = caster;
            _mechanicLogics = mechanics;
        }

        public void InvokeAbility(IActor targetSelected)
        {
            _mechanicLogics.ForEach(mechanicLogic => mechanicLogic.Invoke(targetSelected));
        }
    }
}