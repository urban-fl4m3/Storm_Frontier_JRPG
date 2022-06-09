using System;
using SF.Battle.Common;
using SF.Common.Actors;
using SF.Common.Actors.Components.Animations;
using SF.Common.Actors.Components.Stats;
using SF.Common.Actors.Components.Transform;
using SF.Game;

namespace SF.Battle.Actors
{
    public class BattleActor : Actor
    {
        public BattleMetaData MetaData { get; private set; }
        public int Level => MetaData.Info.Level;
        public Team Team => MetaData.Team;

        public void Init(IServiceLocator serviceLocator, BattleMetaData metaData)
        {
            MetaData = metaData;
            
            Init(serviceLocator);
        }

        public void PerformAttack(BattleActor target, Action onActionEnds = null)
        {
            var activeActorTransform = Components.Get<TransformComponent>();
            var animationComponent = Components.Get<BattleAnimationComponent>();
            var place = target.Components.Get<PlaceholderComponent>().Point;
            var hpComponent = target.Components.Get<HealthComponent>();
            var startPlace = activeActorTransform.GetPosition();

            activeActorTransform.SetPosition(place.transform.position);
            
            hpComponent.RemoveHealth(10000);
            
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
            ServiceLocator.Logger.Log("Perform guard");
            onActionEnds?.Invoke();
        }
    }
}