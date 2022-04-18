using SF.Battle.Actors;
using UnityEngine;

namespace SF.Battle.Field
{
    public class BattlePlaceholder : MonoBehaviour
    {
        public bool IsEmpty => _actor == null;

        private BattleActor _actor;
        
        public void PlaceActor(BattleActor actor)
        {
            if (!IsEmpty)
            {
                return;
            }
            
            _actor = actor;
            actor.transform.position = transform.position;
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