using UnityEngine;

namespace SF.Battle.Abilities
{
    [CreateAssetMenu(fileName = nameof(PassiveBattleAbilityData), menuName = "Battle/Passive Ability")]
    public class PassiveBattleAbilityData : BattleAbilityData
    {
        public override bool IsPassive => true;
    }
}