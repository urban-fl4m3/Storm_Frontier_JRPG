using System;
using SF.Battle.Abilities;
using SF.Battle.Common;
using SF.Battle.Damage;
using SF.Battle.Stats;
using SF.Common.Actors;
using SF.Common.Actors.Abilities;
using SF.Common.Actors.Weapon;
using SF.Game;
using SF.Game.Data;
using SF.Game.Worlds;
using UnityEngine;

namespace SF.Battle.Actors
{
    public class BattleActor : SceneActor, IHealthChangeable, IStatHolder
    {
        public event Action TurnPassed;
        
        public BattleMetaData MetaData { get; private set; }
        public int Level => MetaData.Info.Level;
        public Team Team => MetaData.Team;

        private StatContainer _stats;
        private HealthCalculator _healthCalculator;

        //todo remove, pass into actor as metadata
        [SerializeField] private StatScaleConfig _statScaleConfig;
        
        public void Init(IServiceLocator serviceLocator, BattleMetaData metaData, IWorld world)
        {
            MetaData = metaData;
            
            var characterData = MetaData.Info.Config;
            
            _stats = new StatContainer(Level,
                characterData.BaseData,
                characterData.AdditionalMainStats, 
                characterData.ProfessionData.Tiers, 
                characterData.ProfessionData.AdditionalPrimaryStats, 
                _statScaleConfig);
            
            Init(serviceLocator, world);
            
            _healthCalculator = new HealthCalculator(this);
        }

        public void PerformAttack(SceneActor target, Action onActionEnds = null)
        {
            PlaceInFrontOf(target);

            Components.Get<WeaponComponent>().InvokeAttack(target, () =>
            {
                onActionEnds?.Invoke();
                SetPosition(Components.Get<PlaceholderComponent>().Placeholder.position);
            });
        }
        
        public void PerformSkill(ActiveBattleAbilityData abilityData, SceneActor target, Action onActionEnds = null)
        {
            var startLookAtVector = transform.forward;
            
            PlaceInFrontOf(target);
            
           Components.Get<AbilityComponent>().InvokeSkill(abilityData, target, () =>
           {
               onActionEnds?.Invoke();
               SetPosition(Components.Get<PlaceholderComponent>().Placeholder.position);
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
            Components.Get<PlaceholderComponent>().SetPlaceholder(placeholder);
            SyncWith(placeholder);
        }

        public StatContainer GetStatContainer()
        {
            return _stats;
        }
    }
}