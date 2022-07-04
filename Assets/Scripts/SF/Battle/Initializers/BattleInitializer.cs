using System.Collections.Generic;
using System.Linq;
using SF.Battle.Data;
using SF.Battle.Field;
using SF.Common.Cinemachine;
using SF.Game;
using SF.Game.Data.Characters;
using SF.Game.Initializers;
using SF.Game.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SF.Battle.Initializers
{
    public class BattleInitializer : SerializedMonoBehaviour, IWorldInitializer
    {
        [SerializeField] private BattleField _field;
        [SerializeField] private List<GameCharacterConfig> _enemies;

        public IWorld CreateWorld(IServiceLocator serviceLocator, IPlayerState playerState, CinemachineModel model)
        {
            return new BattleWorld(serviceLocator, playerState, _field, GetEnemiesInfo(), model);
        }

        private IEnumerable<BattleCharacterInfo> GetEnemiesInfo()
        {
            return _enemies.Select(enemyData 
                => new BattleCharacterInfo(enemyData, 1));
        }
    }
}