using System;
using System.Collections.Generic;
using SF.Battle.Abilities.Mechanics.Data;
using SF.Battle.Effects;
using SF.Game.Factories;

namespace SF.Battle.Abilities.Factories
{
    public class EffectsFactory : WorldTypedFactory<IEffectData, IEffect>
    {
        protected override Dictionary<Type, Type> DiscriminatedTypes => new Dictionary<Type, Type>
        {
            {typeof(StatEffectMechanicData), typeof(StatEffect)}
        };
    }
}