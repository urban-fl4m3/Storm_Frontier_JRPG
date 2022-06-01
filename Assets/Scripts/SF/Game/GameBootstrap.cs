using System.Collections.Generic;
using SF.Battle.Data;
using SF.Game.Data.Characters;
using SF.Game.Initializers;
using SF.Game.Player;
using SF.Game.States;
using SF.UI.Controller;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace SF.Game
{
    public class GameBootstrap : SerializedMonoBehaviour
    {
        [OdinSerialize] private IWorldInitializer _worldInitializer;
        [OdinSerialize] private IWindowController _windowController;
        [SerializeField] private List<GameCharacterConfig> _playerCharacters;
        
        private GameStateMachine _gameStateMachine;
        private IServiceLocator _serviceLocator;
        private IPlayerState _playerState;
        private IWorld _currentWorld;
        
        private void Start()
        {
            _serviceLocator = new ServiceLocator(_windowController);
            _playerState = new PlayerState();

            _serviceLocator.TickProcessor.Start();

            _gameStateMachine = new GameStateMachine(_serviceLocator);
            _currentWorld = _worldInitializer.CreateWorld(_serviceLocator, _playerState);
            
            AddDebugCharacterToPlayer();
            
            _gameStateMachine.SetWorld(_currentWorld);
            _gameStateMachine.SetState(GameStateType.WorldBattle);
        }

        private void AddDebugCharacterToPlayer()
        {
            foreach (var playerCharacter in _playerCharacters)
            {
                _playerState.Loadout.AddCharacter(new BattleCharacterInfo(playerCharacter, 98));
            }
        }
    }
}