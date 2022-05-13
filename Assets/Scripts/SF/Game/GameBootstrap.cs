using System.Collections.Generic;
using SF.Battle.Data;
using SF.Game.Data.Characters;
using SF.Game.Initializers;
using SF.Game.Player;
using SF.Game.States;
using SF.UI.Controller;
using SF.UI.Data;
using SF.UI.Windows;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace SF.Game
{
    public class GameBootstrap : SerializedMonoBehaviour
    {
        private BattleWindowController _battleWindowController;
        private IServiceLocator _serviceLocator;
        private IPlayerState _playerState;

        [OdinSerialize] private IWorldInitializer _worldInitializer;
        [OdinSerialize] private IWindowController _windowController;
        [SerializeField] private List<GameCharacterConfig> _playerCharacters;

        private void Start()
        {
            _serviceLocator = new ServiceLocator();
            _playerState = new PlayerState();
            
            AddDebugCharacterToPlayer();
            CreateBattleWindow();

            _serviceLocator.TickProcessor.Start();

            var stateMachine = new GameStateMachine(_serviceLocator);
            var world = _worldInitializer.GetWorld(_serviceLocator, _playerState);
            stateMachine.ChangeWorld(world);
            stateMachine.SetState(GameStateType.WorldBattle);
        }

        private void CreateBattleWindow()
        {
            var window = Instantiate((BattleWindow) _windowController.Create(WindowType.Battle));

            _battleWindowController = new BattleWindowController(window);
            _battleWindowController.Init();
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