using System.Collections.Generic;
using SF.Battle.Actors;
using SF.Battle.Common;
using SF.Battle.Data;
using SF.Battle.Field;
using SF.Common.Cinemachine;
using SF.Game.Player;

namespace SF.Game
{
    public class BattleWorld : BaseWorld
    {
        public BattleField Field { get; }
        public IEnumerable<BattleActor> ActingActors => _battleActorRegisterer.ActingActors;
        public CinemachineModel CameraModel;
        
        private readonly BattleActorFactory _battleActorFactory;
        private readonly BattlleActorRegisterer _battleActorRegisterer;
        private readonly IEnumerable<BattleCharacterInfo> _enemiesData;

        public BattleWorld(
            IServiceLocator serviceLocator, 
            IPlayerState playerState,
            BattleField field,
            IEnumerable<BattleCharacterInfo> enemiesData,
            CinemachineModel model) : base(serviceLocator, playerState)
        {
            Field = field;
            CameraModel = model;
            
            _enemiesData = enemiesData;
            _battleActorRegisterer = new BattlleActorRegisterer(serviceLocator.Logger);
            _battleActorFactory = new BattleActorFactory(_battleActorRegisterer, serviceLocator);
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
                var actor = _battleActorFactory.Create(enemyInfo.Config.BattleActor, meta, this);

                if (actor == null) continue;

                var placeholder = Field.GetEmptyPlaceholder(team);
                placeholder.PlaceActor(actor);
            }
        }
    }
}