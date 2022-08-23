using System.Linq;
using SF.Battle.Common;
using SF.Battle.Field;
using SF.Battle.Turns;
using SF.Common.Actors;
using SF.Common.Camera.Cinemachine;
using SF.Game;
using UnityEngine;

namespace SF.Battle.Camera
{
    public class BattleCameraController : CinemachineController
    {
        private readonly IBattleActorsHolder _actorsHolder;
        private readonly BattleField _battleField;

        private readonly ITurnAction _playerTurnAction;
        private readonly ITurnAction _enemyTurnAction;

        public BattleCameraController(CinemachineView view, IBattleActorsHolder actorsHolder, TurnManager turnManager,
            BattleField battleField) : base(view)
        {
            _actorsHolder = actorsHolder;
            _battleField = battleField;

            _playerTurnAction = turnManager.GetTurnAction(Team.Player);
            _enemyTurnAction = turnManager.GetTurnAction(Team.Enemy);

            _playerTurnAction.TurnStarted += OnPlayerTurnStarted;
            _playerTurnAction.TurnCompleted += OnPlayerTurnEnd;
            _playerTurnAction.ActorSelected += OnEnemyActorSelected;
            
            _enemyTurnAction.TurnStarted += OnEnemyTurnStarted;
            _enemyTurnAction.ActorSelected += OnFriendlyActorSelected;
        }

        public void Disable()
        {
            _playerTurnAction.TurnStarted -= OnPlayerTurnStarted;
            _playerTurnAction.TurnCompleted -= OnPlayerTurnEnd;
            _playerTurnAction.ActorSelected -= OnEnemyActorSelected;
            
            _enemyTurnAction.TurnStarted -= OnEnemyTurnStarted;
            _enemyTurnAction.ActorSelected -= OnFriendlyActorSelected;
        }

        private void OnPlayerTurnStarted()
        {
            var cinemachineComponent = _actorsHolder.ActingActor.Components.Get<CinemachineTargetComponent>();
            
            SetPosition(cinemachineComponent.CameraPosition);
            SetTarget(cinemachineComponent.LookAtPosition, 0);
            SetTarget(_battleField.Center, 1);
        }

        private void OnPlayerTurnEnd()
        {
            Clear();
        }

        private void OnEnemyTurnStarted()
        {
            var actingActor = _actorsHolder.ActingActor;
            
            var cinemachineComponent = actingActor.Components.Get<CinemachineTargetComponent>();
            var playerLookAtPosition = _actorsHolder.GetTeamActors(Team.Player).FirstOrDefault()
                ?.Components
                .Get<CinemachineTargetComponent>().LookAtPosition;
            
            actingActor.Components.Get<PlaceholderComponent>().SetSelected(true);
            
            SetPosition(cinemachineComponent.CameraPosition);
            SetTarget(cinemachineComponent.LookAtPosition, 0);
            SetTarget(playerLookAtPosition, 1);
        }

        private void OnFriendlyActorSelected(IActor selected)
        {
            var lookAtPosition = selected.Components.Get<CinemachineTargetComponent>().LookAtPosition;
            SetTarget(lookAtPosition, 1);
        }

        private void OnEnemyActorSelected(IActor selected)
        {
            Transform target = null;

            if (selected != null)
            {
                target = selected.Components.Get<CinemachineTargetComponent>().LookAtPosition;
            }
            
            SetTarget(target, 1);
        }
    }
}