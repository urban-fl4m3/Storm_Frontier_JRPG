using System;
using System.Collections.Generic;
using SF.Battle.Abilities.Mechanics.Data;
using SF.Battle.Abilities.Mechanics.Logic;
using SF.Common.Data;
using SF.Game.Factories;

namespace SF.Battle.Abilities.Factories
{
    public class MechanicsFactory : WorldTypedFactory<IMechanicData, IMechanicLogic>
    {
        protected override Dictionary<Type, Type> DiscriminatedTypes => new Dictionary<Type, Type>
        {
            { typeof(DamageMechanicData), typeof(DamageMechanicLogic) }
        };

        protected override void OnInstantiate(IMechanicData @from, IMechanicLogic instance, IDataProvider dataProvider)
        {
            //You must create rule here, you have data (from).
            base.OnInstantiate(from, instance, dataProvider);
            // instance.SetData(from, rule);
        }
    }
}