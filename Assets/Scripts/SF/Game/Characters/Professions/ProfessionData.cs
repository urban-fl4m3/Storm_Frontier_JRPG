using System.Collections.Generic;
using SF.Game.Stats;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace SF.Game.Characters.Professions
{
    [CreateAssetMenu(fileName = "New Profession Config", menuName = "Game/Configs/Profession")]
    public class ProfessionData : SerializedScriptableObject
    {
        [SerializeField] private string _name;
        [OdinSerialize] private Dictionary<PrimaryStat, StatData> _stats;

        public string Name => _name;
        public IReadOnlyDictionary<PrimaryStat, StatData> Stats => _stats;
    }
}