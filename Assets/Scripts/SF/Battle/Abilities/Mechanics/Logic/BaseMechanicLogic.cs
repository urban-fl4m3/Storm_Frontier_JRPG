using SF.Battle.Abilities.Common;
using SF.Battle.Abilities.Mechanics.Data;
using SF.Common.Actors;
using SF.Common.Data;
using SF.Game;

namespace SF.Battle.Abilities.Mechanics.Logic
{
    public abstract class BaseMechanicLogic<TMechanicData> : IMechanicLogic where TMechanicData : IMechanicData
    {
        protected TMechanicData Data { get; private set; }
        protected MechanicPick Pick { get; private set; }
        protected IServiceLocator ServiceLocator { get; private set; }
        
        public void SetFactoryMeta(IDataProvider dataProvider)
        {
            if (dataProvider != null)
            {
                ServiceLocator = dataProvider.GetData<IWorld>().ServiceLocator;
            }
        }
        
        public void SetData(IMechanicData data, MechanicPick pick)
        {
            Pick = pick;

            if (!(data is TMechanicData mechanicData))
            {
                ServiceLocator.Logger.LogError($"Wrong data {data} for skill {GetType()}");
                return;
            }
            
            Data = mechanicData;
            OnDataSet(mechanicData);
        }
        
        public abstract void Invoke(IActor actor);
        
        protected abstract void OnDataSet(TMechanicData data);
    }
}