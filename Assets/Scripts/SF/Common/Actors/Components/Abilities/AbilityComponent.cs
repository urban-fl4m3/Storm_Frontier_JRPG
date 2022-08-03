using System;
using System.Collections.Generic;
using System.Linq;
using SF.Battle.Abilities;
using SF.Battle.Abilities.Factories;
using SF.Battle.Actions;
using SF.Battle.Actors;
using SF.Common.Actors.Actions;
using SF.Common.Actors.Components.Stats;
using SF.Common.Data;

namespace SF.Common.Actors.Abilities
{
    public class AbilityComponent : ActorComponent
    {
        public IEnumerable<ActiveBattleAbilityData> AbilitiesData => _abilities.Keys;
        
        private readonly Dictionary<ActiveBattleAbilityData, BattleAbility> _abilities = new();

        private ActionControllerComponent _actionControllerComponent;
        private ManaComponent _manaComponent;
        private AbilityAction _abilityAction;

        protected override void OnInit()
        {
            if (Owner is not BattleActor caster)
            {
                ServiceLocator.Logger.LogError($"Actor {Owner} is not a battle actor! Cannot initialize skill" +
                                               $"component further");
                return;
            }
            
            _actionControllerComponent = Owner.Components.Get<ActionControllerComponent>();
            _manaComponent = Owner.Components.Get<ManaComponent>();
            _abilityAction = new AbilityAction();
            
            var mechanicsFactory = ServiceLocator.FactoryHolder.Get<MechanicsFactory>();
            
            foreach (var abilityData in caster.MetaData.Info.Config.Abilities)
            {
                var mechanicLogics = abilityData.MechanicsData
                    .Select(mechanicData => mechanicsFactory.Create(mechanicData, Owner.World, new DataProvider(ServiceLocator)))
                    .ToList();

                _abilities.Add(abilityData, new BattleAbility(caster, abilityData, mechanicLogics, abilityData.Pick));
            }
            
            base.OnInit();
        }

        public BattleAbility GetAbilityData(ActiveBattleAbilityData abilityData)
        {
            if (!_abilities.ContainsKey(abilityData))
            {
                ServiceLocator.Logger.LogWarning($"Actor {Owner} doesn't have ability {abilityData.Name}");
                return null;   
            }

            return _abilities[abilityData];
        }

        public bool CanInvoke(ActiveBattleAbilityData data)
        {
            if (!_abilities.ContainsKey(data))
            {
                return false;
            }

            return _manaComponent.Current.Value >= data.ManaCost;
        }
        
        public void InvokeSkill(ActiveBattleAbilityData abilityData, IActor target, Action onActionComplete = null)
        {
            if (!_abilities.ContainsKey(abilityData))
            {
                ServiceLocator.Logger.LogWarning($"Actor {Owner} doesn't have ability {abilityData.Name}");
                return;
            }

            if (CanInvoke(abilityData))
            {
                _abilityAction.Target = target;
                _abilityAction.Ability = _abilities[abilityData];
                _actionControllerComponent.MakeAction(_abilityAction, onActionComplete);
                _manaComponent.Remove(abilityData.ManaCost);
            }
        }
    }
}