using System.Collections.Generic;
using SF.Battle.Abilities.Mechanics.Logic;
using SF.Battle.Actors;
using SF.Battle.TargetSelection;
using SF.Common.Actors;
using SF.Common.Actors.Components.Stats;
using Sirenix.Utilities;

namespace SF.Battle.Abilities
{
    public class BattleAbility
    {
        public TargetPick Pick { get; }

        private readonly BattleActor _caster;
        private readonly ManaComponent _casterMana;
        private readonly ActiveBattleAbilityData _data;
        private readonly IEnumerable<IMechanicLogic> _mechanicLogics;
        
        public BattleAbility(BattleActor caster, ActiveBattleAbilityData data, 
            IEnumerable<IMechanicLogic> mechanics, TargetPick pick)
        {
            Pick = pick;

            _data = data;
            _caster = caster;
            _mechanicLogics = mechanics;
            _casterMana = _caster.Components.Get<ManaComponent>();
        }

        public void InvokeAbility(IActor targetSelected)
        {
            _mechanicLogics.ForEach(mechanicLogic => mechanicLogic.Invoke(_caster, targetSelected));
        }
    }
}