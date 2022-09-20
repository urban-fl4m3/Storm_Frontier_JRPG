using SF.Battle.TargetSelection;
using UnityEngine;

namespace SF.Battle.Abilities
{
    [CreateAssetMenu(fileName = nameof(ActiveBattleAbilityData), menuName = "Battle/Active Ability")]
    public class ActiveBattleAbilityData : BattleAbilityData
    {
        [SerializeField] private int _manaCost;
        [SerializeField] private float _castTime;
        [SerializeField] private TargetSelectionData _selectionData;

        public int ManaCost => _manaCost;
        public float CastTime => _castTime;
        public TargetPick Pick => _selectionData.Pick;
    }
}