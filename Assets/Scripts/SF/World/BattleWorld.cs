using System.Collections.Generic;
using SF.Battle.Common;
using SF.Battle.Data;
using SF.Battle.Field;
using SF.Game.Player;

namespace SF.Game
{
    public class BattleWorld : BaseWorld
    {
        public readonly BattleField Field;
        public readonly BattleActorRegistrar Actors;
        
        private readonly BattleSceneActorFactory _battleSceneActorFactory;
        private readonly IEnumerable<BattleCharacterInfo> _enemiesData;

        public BattleWorld(
            IServiceLocator serviceLocator, 
            IPlayerState playerState,
            BattleField field,
            IEnumerable<BattleCharacterInfo> enemiesData) : base(serviceLocator, playerState)
        {
            Field = field;
            
            _enemiesData = enemiesData;
            Actors = new BattleActorRegistrar(serviceLocator.Logger);
            _battleSceneActorFactory = new BattleSceneActorFactory(Actors, serviceLocator);
        }

        public override void Run()
        {
            CreateActors(Team.Player, PlayerState.Loadout.GetBattleCharactersData());
            CreateActors(Team.Enemy, _enemiesData);
        }

        private void CreateActors(Team team, IEnumerable<BattleCharacterInfo> enemiesData)
        {
            foreach (var enemyInfo in enemiesData)
            {
                if (!Field.HasEmptyPlaceholder(team)) continue;

                var meta = new BattleMetaData(team, enemyInfo);
                var actor = _battleSceneActorFactory.Create(enemyInfo.Config.BattleActor, meta, this);

                if (actor == null) continue;

                var placeholder = Field.GetEmptyPlaceholder(team);
                placeholder.PlaceActor(actor);
            }
        }
    }
}