using System.Collections.Generic;
using SF.Battle.Abilities;
using SF.Battle.Abilities.Factories;
using SF.Battle.Abilities.Mechanics.Logic;
using SF.Battle.Actors;

namespace SF.Common.Actors.Abilities
{
    public class AbilityComponent : ActorComponent
    {
        private Dictionary<string, BattleAbility> _abilities;

        public void Awake()
        {
            var mechanicsFactory = ServiceLocator.FactoryHolder.Get<MechanicsFactory>();

            if (Owner is BattleActor battleActor)
            {
                foreach (var abilityData in battleActor.MetaData.Info.Config.Abilities)
                {
                    if (!abilityData.IsPassive)
                    {
                        var mechanicLogics = new List<IMechanicLogic>();

                        foreach (var mechanicData in abilityData.MechanicsData)
                        {
                            mechanicLogics.Add(mechanicsFactory.Create(mechanicData, Owner.World));
                        }

                        _abilities.Add(abilityData.Name ,new BattleAbility(Owner, mechanicLogics, abilityData.Pick));
                    }
                }
            }
        }

        public BattleAbility GetBattleAbility(string abilityName)
        {
            if(!_abilities.ContainsKey(abilityName))
                return null;

            return _abilities[abilityName];
        }

        public void InvokeSkill(string abilityName, IActor target)
        {
            if(!_abilities.ContainsKey(abilityName))
                return;

            _abilities[abilityName].InvokeAbility(target);
        }
    }
}