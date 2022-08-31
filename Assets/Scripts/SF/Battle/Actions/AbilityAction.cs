using SF.Battle.Abilities;
using SF.Common.Actors;

namespace SF.Battle.Actions
{
    public class AbilityAction : BattleAction
    {
        public BattleAbility Ability { get; }
        public IActor Target { get; }

        public AbilityAction(BattleAbility ability, IActor target)
        {
            Ability = ability;
            Target = target;
        }
        
        public override void Execute()
        {
            Ability.InvokeAbility(Target);    
        }
    }
}