using System;
using SF.Battle.Common;
using SF.Battle.Stats;
using SF.Common.Actors;
using SF.Common.Actors.Components;
using SF.Common.Actors.Components.Transform;
using SF.Game;
using SF.Game.Characters.Professions;
using SF.Game.Data;
using SF.Game.Stats;
using UnityEngine;

namespace SF.Battle.Actors
{
    public class BattleActor : Actor
    {
        public Team Team { get; private set; }
        public int Level { get; private set; }

        public IReadOnlyStatContainer<MainStat> MainStats => _stats;
        public IReadOnlyStatContainer<PrimaryStat> PrimaryStats => _stats;
        
        private StatContainer _stats;
        private ProfessionData _professionData;

        public void Init(IServiceLocator serviceLocator, BattleMetaData metaData, StatScaleConfig statScaleConfig)
        {
            Init(serviceLocator);

            Team = metaData.Team;
            Level = metaData.Info.Level;

            _professionData = metaData.Info.Config.ProfessionData;

            var characterData = metaData.Info.Config;
            
            _stats = new StatContainer(Level,
                characterData.BaseData,
                characterData.AdditionalMainStats, 
                characterData.ProfessionData.Tiers, 
                characterData.ProfessionData.AdditionalPrimaryStats, 
                statScaleConfig);
        }

        public void PerformAttack(BattleActor target, Action onActionEnds = null)
        {
            var activeActorTransform = Components.Get<TransformComponent>();
            var animationComponent = Components.Get<BattleAnimationComponent>();
            var place = target.Components.Get<PlaceholderComponent>().Point;
            var startPlace = activeActorTransform.GetPosition();

            activeActorTransform.SetPosition(place.transform.position);
            
            animationComponent.ActionEnds += CompleteAttack;
            animationComponent.SetAttackTrigger();
            
            void CompleteAttack()
            {
                activeActorTransform.SetPosition(startPlace);
                
                animationComponent.ActionEnds -= CompleteAttack;
                
                onActionEnds?.Invoke();
            }
        }

        public void PerformSkill(int skillIndex, BattleActor target, Action onActionEnds = null)
        {
            onActionEnds?.Invoke();
        }

        public void PerformUseItem(int itemIndex, BattleActor target, Action onActionEnds = null)
        {
            onActionEnds?.Invoke();
        }

        public void PerformGuard(Action onActionEnds = null)
        {
            Debug.Log("Perform guard");
            onActionEnds?.Invoke();
        }
    }
}