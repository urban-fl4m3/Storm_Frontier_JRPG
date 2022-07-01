using SF.Common.Actors;

namespace SF.Battle.Damage
{
    public class HPChangeBuilder
    {
        public int CalculateDamage(IActor dealer, IHPChangeProvider provider, HPChangeMeta meta)
        {
            //Here we should calculate all damage and weapon stuff. Dodge/Critical etc
            return meta.Amount;
        }

        public int CalculateTotalHeal(IActor dealer, IHPChangeProvider provider, HPChangeMeta meta)
        {
            //Here we should calculate all heal include buff 
            return meta.Amount;
        }
    }
}