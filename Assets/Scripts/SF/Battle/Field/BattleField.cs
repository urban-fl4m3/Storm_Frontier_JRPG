using System.Collections.Generic;
using SF.Game;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace SF.Battle.Field
{
    public class BattleField : SerializedMonoBehaviour
    {
        [OdinSerialize] private Dictionary<Team, List<BattlePlaceholder>> _placeholders;
    }
}