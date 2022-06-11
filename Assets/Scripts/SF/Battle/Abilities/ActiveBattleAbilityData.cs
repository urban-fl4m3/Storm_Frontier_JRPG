using SF.Battle.TargetSelection;
using UnityEngine;

namespace SF.Battle.Abilities
{
    [CreateAssetMenu(fileName = nameof(ActiveBattleAbilityData), menuName = "Battle/Active Ability")]
    public class ActiveBattleAbilityData : BattleAbilityData
    {
        [SerializeField] private CustomTargetSelectionData _selectionData;
        [SerializeField] private float _manaCost;

        public CustomTargetSelectionData SelectionData => _selectionData;
        public float ManaCost => _manaCost;

        public override bool IsPassive => false;
    }
}