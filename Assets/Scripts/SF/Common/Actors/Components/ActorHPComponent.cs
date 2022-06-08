using UniRx;

namespace SF.Common.Actors
{
    public class ActorHPComponent: ActorComponent
    {
        public ReactiveProperty<int> CurrentHP;

        public void SetHP(int hp)
        {
            CurrentHP = new ReactiveProperty<int>(hp);
        }

        public void GetDamage(int damage)
        {
            var currentHP = CurrentHP.Value;
            
            CurrentHP.SetValueAndForceNotify(currentHP - damage); 
        }
    }
}