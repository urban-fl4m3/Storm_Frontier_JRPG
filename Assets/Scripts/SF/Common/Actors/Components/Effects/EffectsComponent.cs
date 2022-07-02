using System.Collections.Generic;
using System.Linq;
using SF.Battle.Abilities.Factories;
using SF.Battle.Abilities.Mechanics.Data;
using SF.Battle.Effects;

namespace SF.Common.Actors.Effects
{
    public class EffectsComponent : ActorComponent
    {
        private EffectsFactory _effectsFactory;

        private readonly Dictionary<IEffectData, HashSet<IEffect>> _appliedEffects 
            = new Dictionary<IEffectData, HashSet<IEffect>>();
        
        protected override void OnInit()
        {
            _effectsFactory = ServiceLocator.FactoryHolder.Get<EffectsFactory>();
            
            base.OnInit();
        }

        public void Apply(IEffectData data, IActor affector)
        {
            if (!_appliedEffects.ContainsKey(data))
            {
                _appliedEffects.Add(data, new HashSet<IEffect>());
            }
            else
            {
                if (!data.IsStackable)
                {
                    var notStackableEffectController = _appliedEffects[data].FirstOrDefault();
                    notStackableEffectController?.Refresh(data);

                    return;
                }
            }
            
            var effect = _effectsFactory.Create(data);

            effect.Finished += HandleEffectFinished;
            effect.Apply(data, Owner, affector);
            
            _appliedEffects[data].Add(effect);

            void HandleEffectFinished()
            {
                effect.Finished -= HandleEffectFinished;
                effect.Cancel();

                _appliedEffects[data].Remove(effect);
            }
        }
    }
}