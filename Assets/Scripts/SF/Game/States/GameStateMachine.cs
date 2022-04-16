using System.Collections.Generic;
using SF.Battle.States;
using SF.Common.Logger;
using SF.Common.States;
using SF.Sea.States;

namespace SF.Game.States
{
    public class GameStateMachine : StateMachine<GameStateType>
    {
        private readonly IWorld _world;
        private readonly IDebugLogger _logger;

        public GameStateMachine(IWorld world, IDebugLogger logger)
        {
            _world = world;
            _logger = logger;
        }

        protected override IEnumerable<KeyValuePair<GameStateType, IState>> GetStatesInfo()
        {
            yield return GetWorldExplarationStateInfo();
            yield return GetWorldBattleStateInfo();
            yield return GetSeaExplarationStateInfo();
            yield return GetSeaBattleStateInfo();
        }

        private KeyValuePair<GameStateType, IState> GetWorldExplarationStateInfo()
        {
            return GetGameStateInfo(GameStateType.WorldExploration, new WorldExplorationState(_world, _logger));
        }

        private KeyValuePair<GameStateType, IState> GetWorldBattleStateInfo()
        {
            return GetGameStateInfo(GameStateType.WorldBattle, new BattleState(_world, _logger));
        }
        
        private KeyValuePair<GameStateType, IState> GetSeaExplarationStateInfo()
        {
            return GetGameStateInfo(GameStateType.SeaExploration, new SeaExplorationState(_world, _logger));
        }

        private KeyValuePair<GameStateType, IState> GetSeaBattleStateInfo()
        {
            return GetGameStateInfo(GameStateType.SeaBattle, new SeaBattleState(_world, _logger));
        }

        private static KeyValuePair<GameStateType, IState> GetGameStateInfo(GameStateType type, IState state)
        {
            return new KeyValuePair<GameStateType, IState>(type, state);
        }
    }
}