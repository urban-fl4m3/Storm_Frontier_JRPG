using System;
using System.Linq;
using SF.Battle.Abilities.Mechanics.Data;
using SF.Battle.Actors;
using SF.Battle.Damage;
using SF.Common.Actors;
using SF.Common.Actors.Components.Animations;
using SF.Common.Actors.Components.Transform;
using SF.Common.Animations;
using Sirenix.Utilities;
using UnityEngine;

namespace SF.Battle.Abilities.Mechanics.Logic
{
    public class DamageMechanicLogic : BaseMechanicLogic<DamageMechanicData>
    {
        public override void Invoke(BattleActor caster, IActor selectedActor, Action onActionComplete = null)
        {
            var targets = GetMechanicTargets(caster, selectedActor);

            var animationComponent = caster.Components.Get<BattleAnimationComponent>();
            var animationEventHandler = caster.Components.Get<AnimationEventHandler>();
            var casterDamageProvider = caster.Components.Get<IDamageProvider>();
            var transformComponent = caster.Components.Get<TransformComponent>();
            
            var damageMeta = new DamageMeta((int) Data.Amount);
            var midlePosition = Vector3.zero;
            
            targets.ForEach(a => midlePosition += a.Components.Get<PlaceholderComponent>().Point.position);
            midlePosition /= targets.Count();
            
            transformComponent.SetPosition(midlePosition);

            animationEventHandler.Subscribe("ActionEvent", DoStuff);
            
            animationComponent.ActionEnds += HandleActionComplete;
            animationComponent.SetAttackTrigger();
            
            void HandleActionComplete()
            {
                animationComponent.ActionEnds -= HandleActionComplete;
                animationEventHandler.Unsubscribe("ActionEvent", DoStuff);
                
                onActionComplete?.Invoke();
            }
            
            void DoStuff(object sender, EventArgs e)
            {
                foreach (var target in targets)
                {
                    if (target is BattleActor battleActor)
                    {
                        battleActor.TakeDamage(caster, casterDamageProvider, damageMeta);
                    }
                }
            }
            
            
        }

        protected override void OnDataSet(DamageMechanicData data)
        {
            
        }
    }
}