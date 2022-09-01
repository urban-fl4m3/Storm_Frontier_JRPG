using System.Collections.Generic;
using SF.Battle.Actors;
using SF.Battle.Camera;
using SF.Battle.Common;
using SF.Battle.Data;
using SF.Battle.Field;
using SF.Common.Camera.Cinemachine;
using SF.Game.Extensions;
using SF.Game.Player;

namespace SF.Game
{
    public class BattleWorld : BaseWorld, IBattleActorsHolder
    {
        public BattleField Field { get; }
        public BattleActor ActingActor { get; private set; }
        public IEnumerable<BattleActor> Actors => _actingActors;

        private readonly CinemachineView _cinemachineView;
        private readonly BattleSceneActorFactory _battleSceneActorFactory;
        private readonly IEnumerable<BattleCharacterInfo> _enemiesData;

        private readonly Dictionary<Team, HashSet<BattleActor>> _teams = new();
        private readonly List<BattleActor> _actingActors = new();
        
        private int _currentActingActorIndex;
        
        public BattleWorld(
            IServiceLocator serviceLocator,
            IPlayerState playerState,
            BattleField field,
            CinemachineView cinemachineView,
            IEnumerable<BattleCharacterInfo> enemiesData) : base(serviceLocator, playerState)
        {
            Field = field;
            _enemiesData = enemiesData;
            _cinemachineView = cinemachineView;

            _battleSceneActorFactory = new BattleSceneActorFactory(serviceLocator);
        }

        public override void Run()
        {
            CreateActors(Team.Player, PlayerState.Loadout.GetBattleCharactersData());
            CreateActors(Team.Enemy, _enemiesData);
            CreateBattleCamera();

            ActingActor = _actingActors[_currentActingActorIndex];
        }
        
        public IEnumerable<BattleActor> GetTeamActors(Team team)
        {
            if (!_teams.ContainsKey(team))
            {
                ServiceLocator.Logger.LogError($"Team {team} doesn't exists in actor registerer");
                return null;
            }

            return _teams[team];
        }

        public IEnumerable<BattleActor> GetOppositeTeamActors(Team team)
        {
            var enemyTeam = team.GetOppositeTeam();

            return GetTeamActors(enemyTeam);
        }
        
        private void CreateActors(Team team, IEnumerable<BattleCharacterInfo> actorsData)
        {
            var placeholders = Field.GetTeamPlaceholders(team);
            var currentPlaceholderIndex = 0;
            
            foreach (var data in actorsData)
            {
                var meta = new BattleMetaData(team, data);
                var actor = Add(data.Config.BattleActor, meta);

                var placeholder = placeholders[currentPlaceholderIndex];
                actor.SetNewPlaceholder(placeholder);

                currentPlaceholderIndex++;
            }
        }

        private BattleActor Add(BattleActor prefab, BattleMetaData meta)
        {
            var actor = _battleSceneActorFactory.Create(prefab, meta, this);
            
            if (!_teams.ContainsKey(meta.Team))
            {
                _teams.Add(meta.Team, new HashSet<BattleActor>());
            }

            _teams[meta.Team].Add(actor);
            _actingActors.Add(actor);

            return actor;
        }

        private void CreateBattleCamera()
        {
            var smartCamera = new BattleCameraController(_cinemachineView);
            ServiceLocator.CameraHolder.Add(smartCamera);
        }
    }
}