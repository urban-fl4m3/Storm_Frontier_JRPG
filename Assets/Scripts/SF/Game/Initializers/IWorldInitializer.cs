using SF.Common.Cinemachine;
using SF.Game.Player;

namespace SF.Game.Initializers
{
    public interface IWorldInitializer
    {
        IWorld CreateWorld(IServiceLocator serviceLocator, IPlayerState playerState, CinemachineModel model);
    }
}