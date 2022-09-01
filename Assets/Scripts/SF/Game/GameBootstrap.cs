using System.Collections.Generic;
using SF.Battle.Abilities.Factories;
using SF.Battle.Data;
using SF.Common.Camera.Cinemachine;
using SF.Common.Data;
using SF.Game.Data.Characters;
using SF.Game.Initializers;
using SF.Game.Player;
using SF.Game.States;
using SF.UI.Factories;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace SF.Game
{
    public class GameBootstrap : SerializedMonoBehaviour
    {
        [OdinSerialize] private IWorldInitializer _worldInitializer;
        [SerializeField] private WindowsFactory _windowsFactory;
        [SerializeField] private List<GameCharacterConfig> _playerCharacters;

        private GameStateMachine _gameStateMachine;
        private CinemachineController _cinemachineController;
        private IServiceLocator _serviceLocator;
        private IPlayerState _playerState;
        
        private void Start()
        {
            InitServiceLocator();
            
            var world = _worldInitializer.CreateWorld(_serviceLocator, _playerState);

            _gameStateMachine = new GameStateMachine(_serviceLocator);
            _gameStateMachine.SetState(GameStateType.WorldBattle, new DataProvider(world));
        }

        private void InitServiceLocator()
        {
            _serviceLocator = new ServiceLocator();

            InitPlayerState();
            InitFactories();
            InitTicks();
        }

        private void InitFactories()
        {
            _serviceLocator.FactoryHolder.Add(new MechanicsFactory());
            _serviceLocator.FactoryHolder.Add(new EffectsFactory());
            _serviceLocator.FactoryHolder.Add(_windowsFactory);
        }

        private void InitTicks()
        {
            _serviceLocator.TickProcessor.Start();
        }

        private void InitPlayerState()
        {
            _playerState = new PlayerState();

            foreach (var playerCharacter in _playerCharacters)
            {
                _playerState.Loadout.AddCharacter(new BattleCharacterInfo(playerCharacter, 1));
            }
        }
    }
}