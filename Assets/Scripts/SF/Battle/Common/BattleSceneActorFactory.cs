using SF.Battle.Actors;
using SF.Common.Actors.Factories;
using SF.Game;

namespace SF.Battle.Common
{
    public class BattleSceneActorFactory : SceneActorFactory
    {
        private readonly BattleActorRegistrar _registrar;
        private readonly IServiceLocator _serviceLocator;

        public BattleSceneActorFactory(
            BattleActorRegistrar registrar,
            IServiceLocator serviceLocator)
        {
            _registrar = registrar;
            _serviceLocator = serviceLocator;
        }

        public BattleActor Create(BattleActor prefab, BattleMetaData metaData, IWorld world)
        {
            return Create(prefab, ActorSpawnData.Default(), metaData, world);
        }
        
        public BattleActor Create(BattleActor prefab, ActorSpawnData spawnData, BattleMetaData metaData, IWorld world)
        {
            var actor = Create(prefab, spawnData);

            if (actor == null)
            {
                _serviceLocator.Logger.LogError($"Instantiated actor for {metaData.Team} is null");
                return null;
            }

            var isAdded = _registrar.Register(actor, metaData.Team);

            if (isAdded)
            {
                actor.Init(_serviceLocator, metaData, world);
            }

            return actor;
        }
    }
}