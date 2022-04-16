using SF.Battle.Field;
using SF.Game;
using SF.Game.Initializers;
using SF.Game.Player;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SF.Battle.Initializers
{
    public class BattleInitializer : SerializedMonoBehaviour, IWorldInitializer
    {
        [SerializeField] private BattleField _field;
        
        public IWorld GetWorld(IServiceLocator serviceLocator, IPlayerState playerState)
        {
            return new BattleWorld(serviceLocator, playerState, _field);
        }
    }
}