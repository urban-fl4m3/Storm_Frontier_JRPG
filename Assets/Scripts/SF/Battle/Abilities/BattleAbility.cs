using System.Collections.Generic;
using SF.Battle.Abilities.Mechanics.Logic;
using SF.Battle.Actors;
using SF.Battle.Stats;
using SF.Battle.TargetSelection;
using SF.Common.Actors;
using SF.Common.Actors.Components.Stats;
using SF.Game.Stats;
using Sirenix.Utilities;

namespace SF.Battle.Abilities
{
    public class BattleAbility
    {
        public TargetPick Pick { get; }

        private readonly BattleActor _caster;
        private readonly ActiveBattleAbilityData _data;
        private readonly IPrimaryStatResource _casterMana;
        private readonly IEnumerable<IMechanicLogic> _mechanicLogics;
        
        public BattleAbility(BattleActor caster, ActiveBattleAbilityData data, 
            IEnumerable<IMechanicLogic> mechanics, TargetPick pick)
        {
            Pick = pick;

            _data = data;
            _caster = caster;
            _mechanicLogics = mechanics;

            var statHolder = _caster.Components.Get<IStatHolder>();
            var statContainer = statHolder.GetStatContainer();
            _casterMana = statContainer.GetStatResourceResolver(PrimaryStat.MP);
        }

        public void InvokeAbility(IActor targetSelected)
        {
            _mechanicLogics.ForEach(mechanicLogic => mechanicLogic.Invoke(_caster, targetSelected));
        }
    }
}