using System.Collections.Generic;
using System.Linq;
using SF.Game;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace SF.Battle.Field
{
    public class BattleField : SerializedMonoBehaviour
    {
        [OdinSerialize] private Dictionary<Team, List<BattlePlaceholder>> _placeholders;

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
    }
}