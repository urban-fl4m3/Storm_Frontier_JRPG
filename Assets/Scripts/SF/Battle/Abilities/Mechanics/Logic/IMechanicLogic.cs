using SF.Battle.Abilities.Common;
using SF.Battle.Abilities.Mechanics.Data;
using SF.Common.Actors;
using SF.Common.Factories;

namespace SF.Battle.Abilities.Mechanics.Logic
{
    public interface IMechanicLogic : IFactoryInstance
    {
        void SetData(IMechanicData data, MechanicPick pick);

        void Invoke(IActor selected);
    }
}