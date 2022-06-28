using System;
using SF.Battle.Abilities.Mechanics.Data;
using SF.Battle.Actors;
using SF.Common.Actors;
using SF.Common.Factories;

namespace SF.Battle.Abilities.Mechanics.Logic
{
    public interface IMechanicLogic : IFactoryInstance
    {
        void SetData(IMechanicData data);

        void Invoke(BattleActor caster, IActor selected, Action onActionComplete = null);
    }
}