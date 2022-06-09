using SF.Battle.Actors;
using SF.Battle.Stats;
using SF.Game.Data;
using SF.Game.Stats;
using UnityEngine;

namespace SF.Common.Actors.Components.Stats
{
    public class StatsContainerComponent : ActorComponent, IReadOnlyStatContainer<MainStat>, IReadOnlyStatContainer<PrimaryStat>,
        IStatContainerConsumer<MainStat>, IStatContainerConsumer<PrimaryStat>
    {
        [SerializeField] private StatScaleConfig _statScaleConfig;
        
        private StatContainer _stats;

        public int GetStat(MainStat stat) => _stats.GetStat(stat);

        public int GetStat(PrimaryStat stat) => _stats.GetStat(stat);

        public void SetStatValue(MainStat stat, int value) => _stats.SetStatValue(stat, value);

        public void AddStatValue(MainStat stat, int value) => _stats.AddStatValue(stat, value);

        public void SetStatValue(PrimaryStat stat, int value) => _stats.SetStatValue(stat, value);

        public void AddStatValue(PrimaryStat stat, int value) => _stats.AddStatValue(stat, value);
        
        protected override void OnInit()
        {
            var battleActor = (BattleActor) Owner;

            if (battleActor == null)
            {
                ServiceLocator.Logger.LogError($"{gameObject} is not a battle actor.");
                return;
            }

            var characterData = battleActor.MetaData.Info.Config;
            
            _stats = new StatContainer(battleActor.Level,
                characterData.BaseData,
                characterData.AdditionalMainStats, 
                characterData.ProfessionData.Tiers, 
                characterData.ProfessionData.AdditionalPrimaryStats, 
                _statScaleConfig);
            
            base.OnInit();
        }
    }
}