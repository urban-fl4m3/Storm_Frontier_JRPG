using SF.Common.Animations;
using UnityEngine;

namespace SF.Common.Actors.Components
{
    public class AnimationComponent : BaseAnimatorController
    {
        [SerializeField] private BaseAnimatorController _animatorController;
        
        [SerializeField] 
        [AnimationParameterAttribute(AnimatorControllerParameterType.Trigger, nameof(_animator))]
        private string _attackTrigger;

        public void SetAttackTrigger()
        {
            SetTrigger(_attackTrigger);
        }
    }
}