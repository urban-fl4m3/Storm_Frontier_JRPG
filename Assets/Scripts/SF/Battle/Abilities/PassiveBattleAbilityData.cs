using SF.Battle.TargetSelection;
using UnityEngine;

namespace SF.Battle.Abilities
{
    [CreateAssetMenu(fileName = nameof(PassiveBattleAbilityData), menuName = "Battle/Passive Ability")]
    public class PassiveBattleAbilityData : BattleAbilityData
    {
        public override TargetPick Pick => TargetPick.Instant;
        public override bool IsPassive => true;
    }
}