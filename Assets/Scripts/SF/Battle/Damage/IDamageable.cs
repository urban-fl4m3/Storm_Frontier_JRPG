using SF.Common.Actors;

namespace SF.Battle.Damage
{
    public interface IDamageable
    {
        void TakeDamage(IActor dealer, IDamageProvider provider, DamageMeta meta);
    }
}