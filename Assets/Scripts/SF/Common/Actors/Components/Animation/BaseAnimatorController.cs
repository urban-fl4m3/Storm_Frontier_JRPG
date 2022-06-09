using UnityEngine;

namespace SF.Common.Actors.Components.Animations
{
    public class BaseAnimatorController : ActorComponent
    {
        [SerializeField] protected Animator _animator;

        protected void SetTrigger(string triggerName)
        {
            _animator.SetTrigger(triggerName);
        }

        protected void SetBool(string boolName, bool value)
        {
            _animator.SetBool(boolName, value);
        }
    }
}