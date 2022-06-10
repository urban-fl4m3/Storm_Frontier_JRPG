using SF.Battle.Actors;
using SF.Common.Actors.Components.Stats;

namespace SF.Battle.Damage
{
    public static class DamageSystem
    {
        public static void GetDamage(this BattleActor injured, int damage)
        {
            var hpComponent = injured.Components.Get<HealthComponent>();
            hpComponent.RemoveHealth(damage);
        }
    }
}