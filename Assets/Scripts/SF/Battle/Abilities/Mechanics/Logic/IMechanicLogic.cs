using SF.Battle.Abilities.Mechanics.Data;
using SF.Common.Factories;

namespace SF.Battle.Abilities.Mechanics.Logic
{
    public interface IMechanicLogic : IFactoryInstance
    {
        void SetData(IMechanicData data);

        void Invoke();
    }
}