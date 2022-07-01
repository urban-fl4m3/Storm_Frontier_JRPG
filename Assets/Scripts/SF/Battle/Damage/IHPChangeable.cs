using SF.Common.Actors;

namespace SF.Battle.Damage
{
    public interface IHPChangeable
    {
        void TakeDamage(IActor dealer, IHPChangeProvider provider, HPChangeMeta meta);
        void TakeHeal(IActor dealer, IHPChangeProvider provider, HPChangeMeta meta);
    }
}