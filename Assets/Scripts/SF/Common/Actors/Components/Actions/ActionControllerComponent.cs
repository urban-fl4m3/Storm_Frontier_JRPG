using System;
using SF.Battle.Actions;
using SF.Common.Actors.Components.Animations;
using SF.Common.Animations;

namespace SF.Common.Actors.Actions
{
    public class ActionControllerComponent : ActorComponent
    {
        private BattleAnimationComponent _battleAnimationComponent;
        private AnimationEventHandler _animationEventHandler;

        protected override void OnInit()
        {
            _battleAnimationComponent = Owner.Components.Get<BattleAnimationComponent>();
            _animationEventHandler = Owner.Components.Get<AnimationEventHandler>();
            
            base.OnInit();
        }

        public void MakeAction(BattleAction battleAction, Action onActionComplete)
        {
            _animationEventHandler.Subscribe("ActionEvent", ExecuteBattleAction);
            
            _battleAnimationComponent.ActionEnds += HandleActionComplete;
            _battleAnimationComponent.SetAttackTrigger();
            
            void HandleActionComplete()
            {
                _battleAnimationComponent.ActionEnds -= HandleActionComplete;
                _animationEventHandler.Unsubscribe("ActionEvent", ExecuteBattleAction);
                
                onActionComplete?.Invoke();
            }

            void ExecuteBattleAction(object sender, EventArgs e)
            {
                battleAction.Execute();
            }
        }
    }
}