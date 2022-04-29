using System.Collections.Generic;
using SF.Battle.Data;
using SF.Game.Data.Characters;
using SF.Game.Initializers;
using SF.Game.Player;
using SF.Game.States;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace SF.Game
{
    public class GameBootstrap : SerializedMonoBehaviour
    {
        private IServiceLocator _serviceLocator;
        private IPlayerState _playerState;

        [OdinSerialize] private IWorldInitializer _worldInitializer;
        [SerializeField] private List<GameCharacterConfig> _playerCharacters;

        private void Start()
        {
            _serviceLocator = new ServiceLocator();
            _playerState = new PlayerState();
            AddDebugCharacterToPlayer();
            
            var stateMachine = new GameStateMachine(_serviceLocator);
            var world = _worldInitializer.GetWorld(_serviceLocator, _playerState);
            stateMachine.ChangeWorld(world);
            stateMachine.SetState(GameStateType.WorldBattle);
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