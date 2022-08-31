using SF.Battle.Stats;
using SF.Common.Actors;
using SF.Common.Actors.Components.Stats;
using SF.Game.Stats;

namespace SF.Battle.Damage
{
    public class HealthCalculator
    {
        private IActor _actor;
        private readonly IPrimaryStatResource _ownerHealth;

        public HealthCalculator(IActor owner)
        {
            _actor = owner;
            var statHolder = owner.Components.Get<IStatHolder>();
            var statContainer = statHolder.GetStatContainer();

            _ownerHealth = statContainer.GetStatResourceResolver(PrimaryStat.HP);
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