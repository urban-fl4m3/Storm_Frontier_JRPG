using System;
using SF.Battle.Abilities;
using SF.Battle.Common;
using SF.Battle.Damage;
using SF.Common.Actors;
using SF.Common.Actors.Abilities;
using SF.Common.Actors.Weapon;
using SF.Game;

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
        
        public void Init(IServiceLocator serviceLocator, BattleMetaData metaData, IWorld world)
        {
            MetaData = metaData;
            
            Init(serviceLocator, world);

            _weaponComponent = Components.Get<WeaponComponent>();
            _abilityComponent = Components.Get<AbilityComponent>();

            _healthCalculator = new HealthCalculator(this);
        }

        public void PerformAttack(SceneActor target, Action onActionEnds = null)
        {
            var startPlace = GetPosition();
            
            PlaceInFrontOf(target);

            _weaponComponent.InvokeAttack(target, () =>
            {
                onActionEnds?.Invoke();
                SetPosition(startPlace);
            });
        }
        
        public void PerformSkill(ActiveBattleAbilityData abilityData, SceneActor target, Action onActionEnds = null)
        {
            var startPlace = GetPosition();
            var startLookAtVector = transform.forward;
            
            PlaceInFrontOf(target);
            
           _abilityComponent.InvokeSkill(abilityData, target, () =>
           {
               onActionEnds?.Invoke();
               SetPosition(startPlace);
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

        private void PlaceInFrontOf(SceneActor actor)
        {
            if (actor != null)
            {
                var place = actor.Components.Get<PlaceholderComponent>().Point;
                SetPosition(place.transform.position);
                LookAt(actor.GetPosition() - place.position);
            }
        }
    }
}