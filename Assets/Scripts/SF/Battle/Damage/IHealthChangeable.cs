namespace SF.Battle.Damage
{
    public interface IHealthChangeable
    {
        void TakeDamage(DamageMeta meta);
        void TakeHeal(int healAmount);
    }
}