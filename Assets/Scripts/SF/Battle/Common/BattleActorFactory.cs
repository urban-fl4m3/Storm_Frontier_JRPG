using SF.Battle.Actors;
using SF.Common.Actors.Factories;
using SF.Game;
using SF.Game.Data;

namespace SF.Battle.Common
{
    public class BattleActorFactory : ActorFactory
    {
        private readonly BattlleActorRegisterer _registerer;
        private readonly IServiceLocator _serviceLocator;
        private readonly StatScaleConfig _statScaleConfig;

        public BattleActorFactory(
            BattlleActorRegisterer registerer,
            IServiceLocator serviceLocator,
            StatScaleConfig statScaleConfig)
        {
            _registerer = registerer;
            _serviceLocator = serviceLocator;
            _statScaleConfig = statScaleConfig;
        }

        public BattleActor Create(BattleActor prefab, BattleMetaData metaData)
        {
            return Create(prefab, ActorSpawnData.Default(), metaData);
        }
        
        public BattleActor Create(BattleActor prefab, ActorSpawnData spawnData, BattleMetaData metaData)
        {
            var actor = Create(prefab, spawnData);

            if (actor == null)
            {
                _serviceLocator.Logger.LogError($"Instantiated actor for {metaData.Team} is null");
                return null;
            }

            var isAdded = _registerer.AddToTeam(actor, metaData.Team);

            if (isAdded)
            {
                actor.Init(_serviceLocator, metaData, _statScaleConfig);
            }

            return actor;
        }
    }
}