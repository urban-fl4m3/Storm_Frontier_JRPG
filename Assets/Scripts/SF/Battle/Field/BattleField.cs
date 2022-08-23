using System.Collections.Generic;
using SF.Game;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace SF.Battle.Field
{
    public class BattleField : SerializedMonoBehaviour
    {
        [OdinSerialize] private Dictionary<Team, List<Transform>> _placeholders;
        
        [SerializeField] private Transform _fieldCenter;
        [SerializeField] private Transform _activePlayerPlaceholder;

        public Transform Center => _fieldCenter;
        public Transform ActivePlayerPlaceholder => _activePlayerPlaceholder;

        public IReadOnlyList<Transform> GetTeamPlaceholders(Team team)
        {
            return _placeholders.GetValueOrDefault(team);
        }
    }
}