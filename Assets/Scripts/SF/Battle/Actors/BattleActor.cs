using System;
using SF.Battle.Common;
using SF.Battle.Damage;
using SF.Common.Actors;
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

        private readonly DamageBuilder _damageBuilder = new DamageBuilder();
        
        public void Init(IServiceLocator serviceLocator, BattleMetaData metaData)
        {
            MetaData = metaData;
            
            Init(serviceLocator);

            _health = Components.Get<HealthComponent>();
            _transform = Components.Get<TransformComponent>();
            _weaponComponent = Components.Get<WeaponComponent>();
        }

        public void PerformAttack(IActor target, Action onActionEnds = null)
        {
            var place = target.Components.Get<PlaceholderComponent>().Point;
            var startPlace = _transform.GetPosition();

            _transform.SetPosition(place.transform.position);
            
            _weaponComponent.MakeAction(target, () =>
            {
                onActionEnds?.Invoke();
                _transform.SetPosition(startPlace);
            });
        }
        
        public void PerformSkill(int skillIndex, BattleActor target, Action onActionEnds = null)
        {
            onActionEnds?.Invoke();
        }

        public void PerformUseItem(int itemIndex, BattleActor target, Action onActionEnds = null)
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
    }
}