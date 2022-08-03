using System.Collections.Generic;
using SF.Battle.Abilities.Mechanics.Data;
using SF.Battle.Actors;
using SF.Common.Actors;
using SF.Common.Data;
using SF.Game;

namespace SF.Battle.Abilities.Mechanics.Logic
{
    public abstract class BaseMechanicLogic<TMechanicData> : IMechanicLogic where TMechanicData : IMechanicData
    {
        protected TMechanicData Data { get; private set; }
        protected BattleWorld World { get; private set; }
        protected IServiceLocator ServiceLocator { get; private set; }
        
        public void SetFactoryMeta(IDataProvider dataProvider)
        {
            if (dataProvider != null)
            {
                World = dataProvider.GetData<BattleWorld>();
                ServiceLocator = dataProvider.GetData<IServiceLocator>();
            }
        }
        
        public void SetData(IMechanicData data)
        {
            if (data is not TMechanicData mechanicData)
            {
                ServiceLocator.Logger.LogError($"Wrong data {data} for skill {GetType()}");
                return;
            }
            
            Data = mechanicData;
            OnDataSet(mechanicData);
        }
        
        public abstract void Invoke(BattleActor caster, IActor selected);
        
        protected abstract void OnDataSet(TMechanicData data);

        protected IEnumerable<IActor> GetMechanicTargets(BattleActor caster, IActor selectedActor)
        {
            var targets = new List<IActor>();

            //This is a test and cringe variant...
            switch (Data.Pick)
            {
                case MechanicPick.All:
                {
                    targets.AddRange(World.Actors.ActingActors);
                    break;
                }

                case MechanicPick.Self:
                {
                    targets.Add(caster);
                    break;
                }

                case MechanicPick.AllyTeam:
                {
                    targets.AddRange(World.Actors.GetTeamActors(caster.Team));
                    break;
                }

                case MechanicPick.OppositeTeam:
                {
                    targets.AddRange(World.Actors.GetEnemyTeamActors(caster.Team));
                    break;
                }

                case MechanicPick.DontOverride:
                {
                    targets.Add(selectedActor);
                    break;
                }
            }

            return targets;
        }
    }
}