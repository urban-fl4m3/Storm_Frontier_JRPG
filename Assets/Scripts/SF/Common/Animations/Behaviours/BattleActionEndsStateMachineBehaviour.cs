using System;
using UnityEngine;

namespace SF.Common.Animations.Behaviours
{
    public class BattleActionEndsStateMachineBehaviour : StateMachineBehaviour
    {
        public event Action ActionEnds;
        
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            base.OnStateExit(animator, stateInfo, layerIndex);
            ActionEnds?.Invoke();
        }
    }
}