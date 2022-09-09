using System.Collections.Generic;
using System.Linq;
using SF.Battle.Data;
using SF.Battle.Field;
using SF.Common.Camera.Cinemachine;
using SF.Common.Data;
using SF.Game.Data.Characters;
using SF.Game.Initializers;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SF.Battle.Initializers
{
    public class BattleInitializer : SerializedMonoBehaviour, IWorldInitializer
    {
        [SerializeField] private BattleField _field;
        [SerializeField] private CinemachineView _cinemachineView;
        [SerializeField] private List<GameCharacterConfig> _enemies;

        public IDataProvider GetWorldData()
        {
            return new DataProvider(_field, _cinemachineView, GetEnemiesInfo());
        }
        
        private IEnumerable<BattleCharacterInfo> GetEnemiesInfo()
        {
            return _enemies.Select(enemyData 
                => new BattleCharacterInfo(enemyData, 1));
        }
    }
}