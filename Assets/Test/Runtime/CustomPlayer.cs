using Sirenix.OdinInspector;
using UnityEngine;

namespace Test.Runtime
{
    public class CustomPlayer : MonoBehaviour
    {
        [SerializeField] private PlayerDeBufStatus _deBufStatus;

        private IDeBufProvider _provider;

        public void Start()
        {
            _provider = new PlayerDebugContainer();

            _deBufStatus = _provider.GetDeBuf();
        }

        [Button]
        public void SetNextTurn()
        {
            _provider.NextTurn();

            _deBufStatus = _provider.GetDeBuf();
        }

        [Button]
        private void AddShock(int capacity)
        {
            AddDeBuf(DeBufTypes.Shocked, capacity);
        }
        
        [Button]
        private void AddStan(int capacity)
        {
            AddDeBuf(DeBufTypes.Staned, capacity);
        }
        
        
        private void AddDeBuf(DeBufTypes deBufTypes, int capacity)
        {
            _provider = new DeBufEffect(_provider, capacity, deBufTypes);
            _deBufStatus = _provider.GetDeBuf();
        }
    }
}