using SF.Battle.Actors;
using SF.Game.Characters.Professions;
using SF.Game.Stats;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace SF.Game.Data.Characters
{
    [CreateAssetMenu(fileName = "New Character Configuration", menuName = "Game/Character")]
    public class GameCharacterConfig : SerializedScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private DefaultCharactersConfig _baseData;
        [SerializeField] private BattleActor _battleActor;
        [SerializeField] private ProfessionData _professionData;
        [OdinSerialize] private StatContainerData<MainStat> _additionalMainStats;

        public string Name => _name;
        public BattleActor BattleActor => _battleActor;
        public DefaultCharactersConfig BaseData => _baseData;
        public ProfessionData ProfessionData => _professionData;
        public StatContainerData<MainStat> AdditionalMainStats => _additionalMainStats;
    }
}