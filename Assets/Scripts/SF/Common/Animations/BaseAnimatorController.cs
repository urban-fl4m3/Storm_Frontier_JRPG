using Sirenix.OdinInspector;
using UnityEngine;

namespace SF.Common.Animations
{
    public class BaseAnimatorController : SerializedMonoBehaviour
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