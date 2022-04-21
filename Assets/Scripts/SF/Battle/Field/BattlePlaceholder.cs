using SF.Battle.Actors;
using SF.Common.Actors.Components;
using UnityEngine;

namespace SF.Battle.Field
{
    public class BattlePlaceholder : MonoBehaviour
    {
        [SerializeField] private Transform _directionTransform;
        
        public bool IsEmpty => _actor == null;

        private BattleActor _actor;
        
        public void PlaceActor(BattleActor actor)
        {
            if (!IsEmpty)
            {
                return;
            }
            
            _actor = actor;
            actor.Components.Get<TransformComponent>().SetPosition(transform.position);
            actor.Components.Get<RotationComponent>().LookAt(_directionTransform.position - transform.position);
        }

        public void Release()
        {
            if (IsEmpty)
            {
                return;
            }

            _actor = null;
        }
    }
}