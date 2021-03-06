using System.Collections.Generic;
using SF.Battle.Actors;
using SF.Battle.Common;
using SF.Battle.Data;
using SF.Battle.Field;
using SF.Game.Player;

namespace SF.Game
{
    public class BattleWorld : BaseWorld
    {
        public BattleField Field { get; }
        public IEnumerable<BattleActor> ActingActors => _battlleActorRegisterer.ActingActors;
        
        private readonly BattleActorFactory _battleActorFactory;
        private readonly BattlleActorRegisterer _battlleActorRegisterer;
        private readonly IEnumerable<BattleCharacterInfo> _enemiesData;

        public BattleWorld(
            IServiceLocator serviceLocator, 
            IPlayerState playerState,
            BattleField field,
            IEnumerable<BattleCharacterInfo> enemiesData) : base(serviceLocator, playerState)
        {
            Field = field;
            
            _enemiesData = enemiesData;
            _battlleActorRegisterer = new BattlleActorRegisterer(serviceLocator.Logger);
            _battleActorFactory = new BattleActorFactory(_battlleActorRegisterer, serviceLocator);
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
                var actor = _battleActorFactory.Create(enemyInfo.Config.BattleActor, meta);

                if (actor == null) continue;

                var placeholder = Field.GetEmptyPlaceholder(team);
                placeholder.PlaceActor(actor);
            }
        }
    }
}