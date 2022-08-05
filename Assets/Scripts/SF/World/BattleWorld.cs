﻿using System.Collections.Generic;
using SF.Battle.Camera;
using SF.Battle.Common;
using SF.Battle.Data;
using SF.Battle.Field;
using SF.Common.Camera.Cinemachine;
using SF.Game.Player;

namespace SF.Game
{
    public class BattleWorld : BaseWorld
    {
        public BattleField Field => _field;
        public IBattleActorsHolder ActorsHolder => _actorsRegistrar;
        
        private readonly BattleField _field;
        private readonly CinemachineView _cinemachineView;
        private readonly BattleActorRegistrar _actorsRegistrar;
        private readonly BattleSceneActorFactory _battleSceneActorFactory;
        private readonly IEnumerable<BattleCharacterInfo> _enemiesData;

        public BattleWorld(
            IServiceLocator serviceLocator,
            IPlayerState playerState,
            BattleField field,
            CinemachineView cinemachineView,
            IEnumerable<BattleCharacterInfo> enemiesData) : base(serviceLocator, playerState)
        {
            _field = field;
            _enemiesData = enemiesData;
            _cinemachineView = cinemachineView;

            _actorsRegistrar = new BattleActorRegistrar(serviceLocator.Logger);
            _battleSceneActorFactory = new BattleSceneActorFactory(_actorsRegistrar, serviceLocator);
        }

        public override void Run()
        {
            CreateActors(Team.Player, PlayerState.Loadout.GetBattleCharactersData());
            CreateActors(Team.Enemy, _enemiesData);
            CreateBattleCamera();
        }

        private void CreateActors(Team team, IEnumerable<BattleCharacterInfo> enemiesData)
        {
            var placeholders = _field.GetTeamPlaceholders(team);
            var currentPlaceholderIndex = 0;
            
            foreach (var enemyInfo in enemiesData)
            {
                var meta = new BattleMetaData(team, enemyInfo);
                var actor = _battleSceneActorFactory.Create(enemyInfo.Config.BattleActor, meta, this);

                if (actor == null) continue;

                var placeholder = placeholders[currentPlaceholderIndex];
                actor.SetNewPlaceholder(placeholder);

                currentPlaceholderIndex++;
            }
        }

        private void CreateBattleCamera()
        {
            var smartCamera = new BattleCameraController(_cinemachineView, _actorsRegistrar);
            ServiceLocator.CameraHolder.Add(smartCamera);
        }
    }
}