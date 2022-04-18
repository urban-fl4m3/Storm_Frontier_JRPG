using SF.Battle.Actors;
using UnityEngine;

namespace SF.Battle.Data
{
    [CreateAssetMenu(fileName = "New Battle Character Configuration", menuName = "Game/Battle/Character")]
    public class BattleCharacterConfig : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private BattleActor _actor;

        public string Name => _name;
        public BattleActor Actor => _actor;
    }
}