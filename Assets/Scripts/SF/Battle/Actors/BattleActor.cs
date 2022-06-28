using System;
using SF.Battle.Abilities;
using SF.Battle.Common;
using SF.Battle.Damage;
using SF.Common.Actors;
using SF.Common.Actors.Abilities;
using SF.Common.Actors.Components.Stats;
using SF.Common.Actors.Components.Transform;
using SF.Common.Actors.Weapon;
using SF.Game;

namespace SF.Battle.Actors
{
    public class BattleActor : Actor, IDamageable
    {
        public BattleMetaData MetaData { get; private set; }
        public int Level => MetaData.Info.Level;
        public Team Team => MetaData.Team;

        private HealthComponent _health;
        private TransformComponent _transform;
        private WeaponComponent _weaponComponent;
        private AbilityComponent _abilityComponent;

        private readonly DamageBuilder _damageBuilder = new DamageBuilder();
        
        public void Init(IServiceLocator serviceLocator, BattleMetaData metaData, IWorld world)
        {
            MetaData = metaData;
            
            Init(serviceLocator, world);

            _health = Components.Get<HealthComponent>();
            _transform = Components.Get<TransformComponent>();
            _weaponComponent = Components.Get<WeaponComponent>();
            _abilityComponent = Components.Get<AbilityComponent>();
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
            
            PlaceInFrontOf(target);
            
           _abilityComponent.InvokeSkill(abilityData, target, () =>
           {
               onActionEnds?.Invoke();
               _transform.SetPosition(startPlace);
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

        public void TakeDamage(IActor dealer, IDamageProvider provider, DamageMeta meta)
        {
            var calculatedDamage = _damageBuilder.CalculateDamage(dealer, provider, meta);
            _health.RemoveHealth(calculatedDamage);
        }

        private void PlaceInFrontOf(IActor actor)
        {
            if (actor != null)
            {
                var place = actor.Components.Get<PlaceholderComponent>().Point;
                _transform.SetPosition(place.transform.position);
            }
        }
    }
}