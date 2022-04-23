using SF.Battle.Actors;
using SF.Game.Characters.Professions;
using UnityEngine;

namespace SF.Battle.Data
{
    [CreateAssetMenu(fileName = "New Battle Character Configuration", menuName = "Game/Battle/Character")]
    public class BattleCharacterConfig : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private BattleActor _actor;
        [SerializeField] private ProfessionData _professionData;

        public string Name => _name;
        public BattleActor Actor => _actor;
        public ProfessionData ProfessionData => _professionData;
    }
}