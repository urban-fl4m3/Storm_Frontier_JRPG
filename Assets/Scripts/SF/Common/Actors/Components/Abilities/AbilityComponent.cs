using System;
using System.Collections.Generic;
using System.Linq;
using SF.Battle.Abilities;
using SF.Battle.Abilities.Factories;
using SF.Battle.Actions;
using SF.Battle.Actors;
using SF.Battle.Stats;
using SF.Common.Actors.Actions;
using SF.Common.Actors.Components.Stats;
using SF.Common.Data;
using SF.Game.Stats;

namespace SF.Common.Actors.Abilities
{
    public class AbilityComponent : ActorComponent
    {
        public IEnumerable<ActiveBattleAbilityData> AbilitiesData => _abilities.Keys;
        
        private readonly Dictionary<ActiveBattleAbilityData, BattleAbility> _abilities = new();

        private ActionControllerComponent _actionControllerComponent;
        private IPrimaryStatResource _manaResource;

        protected override void OnInit()
        {
            if (Owner is not BattleActor caster)
            {
                ServiceLocator.Logger.LogError($"Actor {Owner} is not a battle actor! Cannot initialize skill" +
                                               $"component further");
                return;
            }
            
            _actionControllerComponent = Owner.Components.Get<ActionControllerComponent>();

            var statHolder = Owner.Components.Get<IStatHolder>();
            var statContainer = statHolder.GetStatContainer();
                
            _manaResource = statContainer.GetStatResourceResolver(PrimaryStat.MP);
            
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

            return _manaResource.Current.Value >= data.ManaCost;
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
                var abilityAction = new AbilityAction(_abilities[abilityData], target);
                
                _actionControllerComponent.MakeAction(abilityAction, onActionComplete);
                _manaResource.Remove(abilityData.ManaCost);
            }
        }
    }
}