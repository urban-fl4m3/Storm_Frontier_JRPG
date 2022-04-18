namespace SF.Common.Actors.Factories
{
    public interface IActorFactory
    {
        TActor Create<TActor>(TActor prefab, ActorSpawnData spawnData) where TActor : Actor;
    }
}