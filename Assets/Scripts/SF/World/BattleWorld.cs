using System.Collections.Generic;
using SF.Battle.Common;
using SF.Battle.Data;
using SF.Battle.Field;
using SF.Game.Player;

namespace SF.Game
{
    public class BattleWorld : BaseWorld
    {
        public BattleField Field { get; }
        public BattlleActorRegisterer Registerer { get; }
        public BattleActorFactory BattleActorFactory { get; }
        
        private readonly IEnumerable<BattleCharacterInfo> _enemiesData;
        
        public BattleWorld(
            IServiceLocator serviceLocator, 
            IPlayerState playerState,
            BattleField field,
            IEnumerable<BattleCharacterInfo> enemiesData) : base(serviceLocator, playerState)
        {
            Field = field;
            
            _enemiesData = enemiesData;
            Registerer = new BattlleActorRegisterer(serviceLocator.Logger);
            BattleActorFactory = new BattleActorFactory(Registerer, serviceLocator);
        }

        public override void Run()
        {
            CreateActors(Team.Enemy, _enemiesData);
        }

        private void CreateActors(Team team, IEnumerable<BattleCharacterInfo> enemiesData)
        {
            foreach (var enemyInfo in enemiesData)
            {
                if (!Field.HasEmptyPlaceholder(team)) continue;
               
                var actor = BattleActorFactory.Create(enemyInfo.Config.Actor, new BattleMetaData(team, enemyInfo.Level));

                if (actor == null) continue;

                var placeholder = Field.GetEmptyPlaceholder(team);
                placeholder.PlaceActor(actor);
            }
        }
    }
}