using System;
using SF.Battle.Abilities;
using SF.Battle.Common;
using SF.Battle.Damage;
using SF.Battle.Stats;
using SF.Common.Actors;
using SF.Common.Actors.Abilities;
using SF.Common.Actors.Components.Status;
using SF.Common.Actors.Weapon;
using SF.Game;
using SF.Game.Data;
using SF.Game.Worlds;
using UnityEngine;

namespace SF.Battle.Actors
{
    public class BattleActor : SceneActor, IHealthChangeable, IStatHolder
    {
        public event Action ActionPerformed = delegate { };

        public int Level => MetaData.Info.Level;
        public Team Team => MetaData.Team;
        
        public BattleMetaData MetaData { get; private set; }
        public StatContainer Stats { get; private set; }

        private BattleStatusComponent _status;
        private HealthCalculator _healthCalculator;

        //todo remove, pass into actor as metadata
        [SerializeField] private StatScaleConfig _statScaleConfig;
        
        public void Init(IServiceLocator serviceLocator, BattleMetaData metaData, IWorld world)
        {
            MetaData = metaData;
            
            var characterData = MetaData.Info.Config;
            
            Stats = new StatContainer(Level,
                characterData.BaseData,
                characterData.AdditionalMainStats, 
                characterData.ProfessionData.Tiers, 
                characterData.ProfessionData.AdditionalPrimaryStats, 
                _statScaleConfig);
            
            Init(serviceLocator, world);
            
            _healthCalculator = new HealthCalculator(this);
            _status = Components.Get<BattleStatusComponent>();
        }

        public void PerformAttack(SceneActor target)
        {
            PlaceInFrontOf(target);

            Components.Get<WeaponComponent>().InvokeAttack(target, () =>
            {
                ActionPerformed();
                SetPosition(Components.Get<PlaceholderComponent>().Placeholder.position);
            });
        }
        
        public void PerformSkill(ActiveBattleAbilityData abilityData, SceneActor target)
        {
            var startLookAtVector = transform.forward;
            
            PlaceInFrontOf(target);
            
           Components.Get<AbilityComponent>().InvokeSkill(abilityData, target, () =>
           {
               ActionPerformed();
               SetPosition(Components.Get<PlaceholderComponent>().Placeholder.position);
               LookAt(startLookAtVector);
           });
        }

        public void PerformUseItem(int itemIndex, SceneActor target)
        {
            ServiceLocator.Logger.Log("Perform use item");
            
            ActionPerformed();
        }

        public void PerformGuard()
        {
            ServiceLocator.Logger.Log("Perform guard");
           
            ActionPerformed();
        }

        public void TakeDamage(DamageMeta meta)
        {
            _healthCalculator.CalculateDamage(meta);
        }

        public void TakeHeal(int amount)
        {
           _healthCalculator.CalculateHeal(amount);
        }

        public StatContainer GetStatContainer()
        {
            return Stats;
        }

        public bool IsDead()
        {
            return _status.State.Value == ActorState.Dead;
        }
    }
}