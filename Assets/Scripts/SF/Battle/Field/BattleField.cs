using System.Collections.Generic;
using System.Linq;
using SF.Game;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace SF.Battle.Field
{
    public class BattleField : SerializedMonoBehaviour
    {
        [OdinSerialize] private Dictionary<Team, List<BattlePlaceholder>> _placeholders;
        [SerializeField] private Transform _fieldCenter;
        [SerializeField] private BattlePlaceholder _activePlayerPlaceholder;

        public Transform Center => _fieldCenter;
        
        public bool HasEmptyPlaceholder(Team team)
        {
            if (!_placeholders.ContainsKey(team)) return false;

            var placeholders = _placeholders[team];
            return placeholders.Any(x => x.IsEmpty);
        }
        
        public BattlePlaceholder GetEmptyPlaceholder(Team team)
        {
            if (!_placeholders.ContainsKey(team)) return null;
            
            var placeholders = _placeholders[team];
            var emptyPlaceholder = placeholders.FirstOrDefault(x => x.IsEmpty);

            return emptyPlaceholder;
        }

        public void ResetTeamPlaceholders(Team team)
        {
            foreach (var placeholder in _placeholders[team])
            {
                placeholder.Reset();
            }
        }

        public BattlePlaceholder GetActiveActorPlaceholder()
        {
            return _activePlayerPlaceholder;
        }
    }
}