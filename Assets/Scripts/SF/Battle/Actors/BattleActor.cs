using System;
using SF.Battle.Common;
using SF.Battle.Damage;
using SF.Common.Actors;
using SF.Common.Actors.Components.Animations;
using SF.Common.Actors.Components.Stats;
using SF.Common.Actors.Components.Transform;
using SF.Common.Animations;
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
            var animationEventHandler = Components.Get<AnimationEventHandler>();
            var place = target.Components.Get<PlaceholderComponent>().Point;
            var startPlace = activeActorTransform.GetPosition();

            activeActorTransform.SetPosition(place.transform.position);
            
            animationEventHandler.Subscribe("ActionEvent", GetDamage);
            
            animationComponent.ActionEnds += CompleteAttack;
            animationComponent.SetAttackTrigger();
            
            void CompleteAttack()
            {
                activeActorTransform.SetPosition(startPlace);
                
                animationComponent.ActionEnds -= CompleteAttack;
                
                onActionEnds?.Invoke();
            }

            void GetDamage(object sender, EventArgs e)
            {
                target.GetDamage(100);
                animationEventHandler.Unsubscribe("ActionEvent", GetDamage);
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