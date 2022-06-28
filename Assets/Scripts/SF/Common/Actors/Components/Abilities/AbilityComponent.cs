using System;
using System.Collections.Generic;
using System.Linq;
using SF.Battle.Abilities;
using SF.Battle.Abilities.Factories;
using SF.Battle.Actions;
using SF.Battle.Actors;
using SF.Common.Actors.Actions;
using SF.Common.Data;

namespace SF.Common.Actors.Abilities
{
    public class AbilityComponent : ActorComponent
    {
        private readonly Dictionary<BattleAbilityData, BattleAbility> _abilities =
            new Dictionary<BattleAbilityData, BattleAbility>();

        private ActionControllerComponent _actionControllerComponent;
        private AbilityAction _abilityAction;

        protected override void OnInit()
        {
            if (!(Owner is BattleActor caster))
            {
                ServiceLocator.Logger.LogError($"Actor {Owner} is not a battle actor! Cannot initialize skill" +
                                               $"component further");
                return;
            }
            
            _actionControllerComponent = Owner.Components.Get<ActionControllerComponent>();
            _abilityAction = new AbilityAction();
            
            var mechanicsFactory = ServiceLocator.FactoryHolder.Get<MechanicsFactory>();
            
            foreach (var abilityData in caster.MetaData.Info.Config.Abilities)
            {
                if (abilityData.IsPassive) continue;
                
                var mechanicLogics = abilityData.MechanicsData
                    .Select(mechanicData => mechanicsFactory.Create(mechanicData, Owner.World, new DataProvider(ServiceLocator)))
                    .ToList();

                _abilities.Add(abilityData, new BattleAbility(caster, mechanicLogics, abilityData.Pick));
            }
            
            base.OnInit();
        }

        public BattleAbility GetAbilityData(BattleAbilityData abilityData)
        {
            if (!_abilities.ContainsKey(abilityData))
            {
                ServiceLocator.Logger.LogWarning($"Actor {Owner} doesn't have ability {abilityData.Name}");
                return null;   
            }

            return _abilities[abilityData];
        }

        public void InvokeSkill(BattleAbilityData abilityData, IActor target, Action onActionComplete = null)
        {
            if (!_abilities.ContainsKey(abilityData))
            {
                ServiceLocator.Logger.LogWarning($"Actor {Owner} doesn't have ability {abilityData.Name}");
                return;
            }

            _abilityAction.Target = target;
            _abilityAction.Ability = _abilities[abilityData];
            _actionControllerComponent.MakeAction(_abilityAction, onActionComplete);
        }
    }
}