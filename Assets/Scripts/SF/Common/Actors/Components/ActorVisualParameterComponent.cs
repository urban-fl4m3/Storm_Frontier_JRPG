using UnityEngine;

namespace SF.Common.Actors
{
    public class ActorVisualParameterComponent: MonoBehaviour
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _name;

        public Sprite Icon => _icon;
        public string Name => _name;
    }
}