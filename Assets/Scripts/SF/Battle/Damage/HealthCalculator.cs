using SF.Common.Actors;
using SF.Common.Actors.Components.Stats;

namespace SF.Battle.Damage
{
    public class HealthCalculator
    {
        private IActor _actor;
        private readonly HealthComponent _ownerHealth;

        public HealthCalculator(IActor owner)
        {
            _actor = owner;
            _ownerHealth = owner.Components.Get<HealthComponent>();
        }
        
        public void CalculateDamage(DamageMeta meta)
        {
            //Here we should calculate all damage and weapon stuff. Dodge/Critical etc
            _ownerHealth.Remove(meta.Amount);
        }

        public void CalculateHeal(int amount)
        {
            //Here we should calculate all heal and buff stuff. Open wounds/Heal negate etc
            _ownerHealth.Add(amount);
        }
    }
}