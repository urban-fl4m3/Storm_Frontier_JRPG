using System.Collections.Generic;
using SF.Battle.States;
using SF.Common.States;
using SF.Sea.States;

namespace SF.Game.States
{
    public class GameStateMachine : StateMachine<GameStateType, GameState>
    {
        private readonly IServiceLocator _serviceLocator;
        
        public GameStateMachine(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
            
            UpdateStates();
        }

        protected override IEnumerable<KeyValuePair<GameStateType, GameState>> GetStatesInfo()
        {
            yield return GetWorldExplarationStateInfo();
            yield return GetWorldBattleStateInfo();
            yield return GetSeaExplarationStateInfo();
            yield return GetSeaBattleStateInfo();
        }

        private KeyValuePair<GameStateType, GameState> GetWorldExplarationStateInfo()
        {
            return GetGameStateInfo(GameStateType.WorldExploration, new WorldExplorationState(_serviceLocator));
        }

        private KeyValuePair<GameStateType, GameState> GetWorldBattleStateInfo()
        {
            return GetGameStateInfo(GameStateType.WorldBattle, new BattleState(_serviceLocator));
        }
        
        private KeyValuePair<GameStateType, GameState> GetSeaExplarationStateInfo()
        {
            return GetGameStateInfo(GameStateType.SeaExploration, new SeaExplorationState(_serviceLocator));
        }

        private KeyValuePair<GameStateType, GameState> GetSeaBattleStateInfo()
        {
            return GetGameStateInfo(GameStateType.SeaBattle, new SeaBattleState(_serviceLocator));
        }

        private static KeyValuePair<GameStateType, GameState> GetGameStateInfo(GameStateType type, GameState state)
        {
            return new KeyValuePair<GameStateType, GameState>(type, state);
        }
    }
}