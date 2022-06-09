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
        public IEnumerable<BattleActor> ActingActors { get; }

        private readonly BattleActorFactory _battleActorFactory;
        private readonly IEnumerable<BattleCharacterInfo> _enemiesData;

        private readonly List<BattleActor> _actors = new List<BattleActor>();
        
        public BattleWorld(
            IServiceLocator serviceLocator, 
            IPlayerState playerState,
            BattleField field,
            IEnumerable<BattleCharacterInfo> enemiesData) : base(serviceLocator, playerState)
        {
            Field = field;
            ActingActors = _actors;
            
            _enemiesData = enemiesData;
            var registerer = new BattlleActorRegisterer(serviceLocator.Logger);
            _battleActorFactory = new BattleActorFactory(registerer, serviceLocator);
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
                
                _actors.Add(actor);
            }
        }
    }
}