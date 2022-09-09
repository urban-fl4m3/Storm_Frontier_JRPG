using System;
using System.Collections.Generic;
using SF.Battle.Abilities.Mechanics.Data;
using SF.Battle.Abilities.Mechanics.Logic;
using SF.Common.Data;
using SF.Game.Worlds.Factories;

namespace SF.Battle.Abilities.Factories
{
    public class MechanicsFactory : WorldTypedFactory<IMechanicData, IMechanicLogic>
    {
        protected override Dictionary<Type, Type> DiscriminatedTypes => new()
        {
            {typeof(DamageMechanicData), typeof(DamageMechanicLogic)},
            {typeof(HealMechanicData), typeof(HealMechanicLogic)},
            {typeof(StatEffectMechanicData), typeof(EffectMechanicLogic)}
        };

        protected override void OnInstantiate(IMechanicData @from, IMechanicLogic instance, IDataProvider dataProvider)
        {
            base.OnInstantiate(from, instance, dataProvider);
            instance.SetData(from);
        }
    }
}