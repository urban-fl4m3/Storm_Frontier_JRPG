using System;
using SF.Battle.Abilities;
using SF.Battle.Common;
using SF.Battle.Damage;
using SF.Common.Actors;
using SF.Common.Actors.Abilities;
using SF.Common.Actors.Components.Transform;
using SF.Common.Actors.Weapon;
using SF.Game;

namespace SF.Battle.Actors
{
    public class BattleActor : Actor, IHealthChangeable, ITurnConsumer
    {
        public event Action TurnPassed;
        
        public BattleMetaData MetaData { get; private set; }
        public int Level => MetaData.Info.Level;
        public Team Team => MetaData.Team;

        private TransformComponent _transform;
        private WeaponComponent _weaponComponent;
        private AbilityComponent _abilityComponent;
        private RotationComponent _rotationComponent;
        
        private HealthCalculator _healthCalculator;
        
        public void Init(IServiceLocator serviceLocator, BattleMetaData metaData, IWorld world)
        {
            MetaData = metaData;
            
            Init(serviceLocator, world);
            
            _transform = Components.Get<TransformComponent>();
            _weaponComponent = Components.Get<WeaponComponent>();
            _abilityComponent = Components.Get<AbilityComponent>();
            _rotationComponent = Components.Get<RotationComponent>();

            _healthCalculator = new HealthCalculator(this);
        }

        public void PerformAttack(IActor target, Action onActionEnds = null)
        {
            var startPlace = _transform.GetPosition();
            
            PlaceInFrontOf(target);

            _weaponComponent.InvokeAttack(target, () =>
            {
                onActionEnds?.Invoke();
                _transform.SetPosition(startPlace);
            });
        }
        
        public void PerformSkill(BattleAbilityData abilityData, IActor target, Action onActionEnds = null)
        {
            var startPlace = _transform.GetPosition();
            var startLookAtVector = _transform.transform.forward;
            
            PlaceInFrontOf(target);
            
           _abilityComponent.InvokeSkill(abilityData, target, () =>
           {
               onActionEnds?.Invoke();
               _transform.SetPosition(startPlace);
               _rotationComponent.LookAt(startLookAtVector);
           });
        }

        public void PerformUseItem(int itemIndex, IActor target, Action onActionEnds = null)
        {
            onActionEnds?.Invoke();
        }

        public void PerformGuard(Action onActionEnds = null)
        {
            ServiceLocator.Logger.Log("Perform guard");
            onActionEnds?.Invoke();
        }

        public void TakeDamage(DamageMeta meta)
        {
            _healthCalculator.CalculateDamage(meta);
        }

        public void TakeHeal(int amount)
        {
           _healthCalculator.CalculateHeal(amount);
        }

        private void PlaceInFrontOf(IActor actor)
        {
            if (actor != null)
            {
                var place = actor.Components.Get<PlaceholderComponent>().Point;
                var actorTransform = actor.Components.Get<TransformComponent>().transform;
                _transform.SetPosition(place.transform.position);
                _rotationComponent.LookAt(actorTransform.position - place.position);
            }
        }
        
        public void EndTurn()
        {
            TurnPassed?.Invoke();    
        }
    }
}