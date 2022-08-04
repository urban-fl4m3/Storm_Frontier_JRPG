using System;
using SF.Battle.Abilities;
using SF.Battle.Common;
using SF.Battle.Damage;
using SF.Common.Actors;
using SF.Common.Actors.Abilities;
using SF.Common.Actors.Weapon;
using SF.Game;
using UnityEngine;

namespace SF.Battle.Actors
{
    public class BattleActor : SceneActor, IHealthChangeable, ITurnConsumer
    {
        public event Action TurnPassed;
        
        public BattleMetaData MetaData { get; private set; }
        public int Level => MetaData.Info.Level;
        public Team Team => MetaData.Team;

        private WeaponComponent _weaponComponent;
        private AbilityComponent _abilityComponent;
        private HealthCalculator _healthCalculator;
        private PlaceholderComponent _placeholderComponent;
        
        public void Init(IServiceLocator serviceLocator, BattleMetaData metaData, IWorld world)
        {
            MetaData = metaData;
            
            Init(serviceLocator, world);

            _weaponComponent = Components.Get<WeaponComponent>();
            _abilityComponent = Components.Get<AbilityComponent>();
            _placeholderComponent = Components.Get<PlaceholderComponent>();

            _healthCalculator = new HealthCalculator(this);
        }

        public void PerformAttack(SceneActor target, Action onActionEnds = null)
        {
            PlaceInFrontOf(target);

            _weaponComponent.InvokeAttack(target, () =>
            {
                onActionEnds?.Invoke();
                SetPosition(_placeholderComponent.Placeholder.position);
            });
        }
        
        public void PerformSkill(ActiveBattleAbilityData abilityData, SceneActor target, Action onActionEnds = null)
        {
            var startLookAtVector = transform.forward;
            
            PlaceInFrontOf(target);
            
           _abilityComponent.InvokeSkill(abilityData, target, () =>
           {
               onActionEnds?.Invoke();
               SetPosition(_placeholderComponent.Placeholder.position);
               LookAt(startLookAtVector);
           });
        }

        public void PerformUseItem(int itemIndex, SceneActor target, Action onActionEnds = null)
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
        
        public void EndTurn()
        {
            TurnPassed?.Invoke();    
        }

        public void SetNewPlaceholder(Transform placeholder)
        {
            _placeholderComponent.SetPlaceholder(placeholder);
            SyncWith(placeholder);
        }
    }
}