using SF.Battle.Actors;
using SF.Common.Actors.Components.Transform;
using UnityEngine;

namespace SF.Battle.Field
{
    public class BattlePlaceholder : MonoBehaviour
    {
        [SerializeField] private Transform _directionTransform;
        
        public bool IsEmpty => _actor == null;

        private TransformComponent _transformComponent;
        private RotationComponent _rotationComponent;
        private BattleActor _actor;
        
        public void PlaceActor(BattleActor actor)
        {
            if (!IsEmpty)
            {
                return;
            }
            
            _transformComponent = actor.Components.Get<TransformComponent>();
            _rotationComponent = actor.Components.Get<RotationComponent>();
            _actor = actor;
            
            Reset();
        }

        public void Reset()
        {
            if (!IsEmpty)
            {
                _transformComponent.SetPosition(transform.position);
                _rotationComponent.LookAt(_directionTransform.position - transform.position);
            }
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