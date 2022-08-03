using SF.Battle.Actors;
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
            
            Reset();
        }

        public void Reset()
        {
            if (!IsEmpty)
            {
                _actor.SetPosition(transform.position);
                _actor.LookAt(_directionTransform.position - transform.position);
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