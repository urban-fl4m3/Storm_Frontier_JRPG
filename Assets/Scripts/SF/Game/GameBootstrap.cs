using SF.Game.Initializers;
using SF.Game.Player;
using SF.Game.States;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace SF.Game
{
    public class GameBootstrap : SerializedMonoBehaviour
    {
        private IServiceLocator _serviceLocator;
        private IPlayerState _playerState;

        [OdinSerialize] private IWorldInitializer _worldInitializer;
        
        private void Start()
        {
            _serviceLocator = new ServiceLocator();
            _playerState = new PlayerState();

            var stateMachine = new GameStateMachine(_serviceLocator);
            var world = _worldInitializer.GetWorld(_serviceLocator, _playerState);
            stateMachine.ChangeWorld(world);
            stateMachine.SetState(GameStateType.WorldBattle);
        }
    }
}