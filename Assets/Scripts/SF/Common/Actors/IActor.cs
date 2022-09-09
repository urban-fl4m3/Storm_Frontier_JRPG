using SF.Game.Worlds;

namespace SF.Common.Actors
{
    public interface IActor
    {
        ActorComponentContainer Components { get; }
        IWorld World { get; }
    }
}