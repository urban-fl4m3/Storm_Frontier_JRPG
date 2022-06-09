using System;
using SF.Common.Animations;
using SF.Common.Animations.Behaviours;
using UnityEngine;

namespace SF.Common.Actors.Components.Animations
{
    public class BattleAnimationComponent : BaseAnimatorController
    {
        public event Action ActionEnds;
            
        [SerializeField] 
        [AnimationParameterAttribute(AnimatorControllerParameterType.Trigger, nameof(_animator))]
        private string _attackTrigger;

        public void SetAttackTrigger()
        {
            SetTrigger(_attackTrigger);
        }

        private void OnEnable()
        {
            var endBehaviours = _animator.GetBehaviours<BattleActionEndsStateMachineBehaviour>();

            foreach (var behaviour in endBehaviours)
            {
                behaviour.ActionEnds += OnActionEnd;
            }
        }

        private void OnDisable()
        {
            var endBehaviours = _animator.GetBehaviours<BattleActionEndsStateMachineBehaviour>();

            foreach (var behaviour in endBehaviours)
            {
                behaviour.ActionEnds -= OnActionEnd;
            }
        }

        private void OnActionEnd()
        {
            ActionEnds?.Invoke();
        }
    }
}