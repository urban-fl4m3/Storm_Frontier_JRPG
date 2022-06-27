using System.Collections.Generic;
using System.Linq;
using SF.Battle.Abilities;
using SF.Battle.Abilities.Factories;
using SF.Battle.Actors;
using SF.Common.Data;

namespace SF.Common.Actors.Abilities
{
    public class AbilityComponent : ActorComponent
    {
        private readonly Dictionary<BattleAbilityData, BattleAbility> _abilities =
            new Dictionary<BattleAbilityData, BattleAbility>();

        protected override void OnInit()
        {
            var mechanicsFactory = ServiceLocator.FactoryHolder.Get<MechanicsFactory>();

            if (!(Owner is BattleActor battleActor))
            {
                ServiceLocator.Logger.LogError($"Actor {Owner} is not a battle actor! Cannot initialize skill" +
                                               $"component further");
                return;
            }
            
            foreach (var abilityData in battleActor.MetaData.Info.Config.Abilities)
            {
                if (abilityData.IsPassive) continue;
                
                var mechanicLogics = abilityData.MechanicsData
                    .Select(mechanicData => mechanicsFactory.Create(mechanicData, Owner.World, new DataProvider(ServiceLocator)))
                    .ToList();

                _abilities.Add(abilityData, new BattleAbility(battleActor, mechanicLogics, abilityData.Pick));
            }
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

        public void InvokeSkill(BattleAbilityData abilityData, IActor target)
        {
            if (!_abilities.ContainsKey(abilityData))
            {
                ServiceLocator.Logger.LogWarning($"Actor {Owner} doesn't have ability {abilityData.Name}");
                return;
            }

            _abilities[abilityData].InvokeAbility(target);
        }
    }
}