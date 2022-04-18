using System.Collections.Generic;
using SF.Battle.Data;
using SF.Battle.Field;
using SF.Game.Player;

namespace SF.Game
{
    public class BattleWorld : BaseWorld
    {
        public BattleField Field { get; }

        private readonly IEnumerable<BattleCharacterInfo> _enemiesData;
        
        public BattleWorld(
            IServiceLocator serviceLocator, 
            IPlayerState playerState,
            BattleField field,
            IEnumerable<BattleCharacterInfo> enemiesData) : base(serviceLocator, playerState)
        {
            _enemiesData = enemiesData;
            Field = field;
        }

        public override void Run()
        {
            CreatePlayerActors();
            CreateEnemyActors(_enemiesData);
        }

        private void CreatePlayerActors()
        {
            
        }

        private void CreateEnemyActors(IEnumerable<BattleCharacterInfo> enemiesData)
        {
            foreach (var enemyInfo in enemiesData)
            {
                
            }
        }
    }
}