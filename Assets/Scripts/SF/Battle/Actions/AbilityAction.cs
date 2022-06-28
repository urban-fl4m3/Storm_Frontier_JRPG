using SF.Battle.Abilities;
using SF.Common.Actors;

namespace SF.Battle.Actions
{
    public class AbilityAction : BattleAction
    {
        public BattleAbility Ability { get; set; }
        public IActor Target { get; set; }
        
        public override void Execute()
        {
            Ability.InvokeAbility(Target);    
        }
    }
}