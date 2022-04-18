using SF.Common.Actors;
using UnityEngine;

namespace SF.Game.Data
{
    [CreateAssetMenu(fileName = "New Character Configuration", menuName = "Game/Data/Character")]
    public class CharacterConfig : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Actor _actor;

        public string Name => _name;
        public Actor Actor => _actor;
    }
}