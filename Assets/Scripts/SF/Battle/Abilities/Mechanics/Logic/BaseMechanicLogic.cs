using SF.Battle.Abilities.Mechanics.Data;
using SF.Common.Data;
using SF.Game;

namespace SF.Battle.Abilities.Mechanics.Logic
{
    public abstract class BaseMechanicLogic<TMechanicData> : IMechanicLogic where TMechanicData : IMechanicData
    {
        protected TMechanicData Data { get; private set; }
        
        public void SetFactoryMeta(IDataProvider dataProvider)
        {
            if (dataProvider != null)
            {
                dataProvider.GetData<IWorld>().ServiceLocator.Logger.Log("HELLO??");
            }
        }
        
        public void SetData(IMechanicData data)
        {
            if (data is TMechanicData mechanicData)
            {
                Data = mechanicData;
                OnDataSet(mechanicData);
            }
        }

        //Invoke mechanic to some targets
        public abstract void Invoke();

        protected abstract void OnDataSet(TMechanicData data);
    }
}