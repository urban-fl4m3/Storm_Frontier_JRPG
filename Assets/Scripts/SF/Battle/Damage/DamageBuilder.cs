using SF.Common.Actors;

namespace SF.Battle.Damage
{
    public class DamageBuilder
    {
        public int CalculateDamage(IActor dealer, IDamageProvider provider, DamageMeta meta)
        {
            //Here we should calculate all damage and weapon stuff. Dodge/Critical etc
            return meta.DamageAmount;
        }
    }
}