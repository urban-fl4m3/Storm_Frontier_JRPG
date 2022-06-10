using SF.Common.Actors;

namespace SF.Battle.Damage
{
    public interface IDamageTaker
    {
        void TakeDamage(IActor dealer, IDamageProvider provider, DamageMeta meta);
    }
}