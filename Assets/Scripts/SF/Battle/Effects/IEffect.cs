using System;
using SF.Battle.Abilities.Mechanics.Data;
using SF.Common.Actors;
using SF.Common.Factories;

namespace SF.Battle.Effects
{
    public interface IEffect : IFactoryInstance
    {
        event Action Finished;
        
        void Apply(IEffectData data, IActor affected, IActor affector);
        void Refresh(IEffectData effectData);
        void Cancel();
    }
}