using System.Collections.Generic;
using System.Linq;
using SF.Battle.Data;
using SF.Battle.Field;
using SF.Game;
using SF.Game.Data;
using SF.Game.Initializers;
using SF.Game.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SF.Battle.Initializers
{
    public class BattleInitializer : SerializedMonoBehaviour, IWorldInitializer
    {
        [SerializeField] private BattleField _field;
        [SerializeField] private List<CharacterConfig> _enemies;

        public IWorld GetWorld(IServiceLocator serviceLocator, IPlayerState playerState)
        {
            return new BattleWorld(serviceLocator, playerState, _field, GetEnemiesInfo());
        }

        private IEnumerable<BattleCharacterInfo> GetEnemiesInfo()
        {
            return _enemies.Select(enemyData => new BattleCharacterInfo(enemyData, 1));
        }
    }
}