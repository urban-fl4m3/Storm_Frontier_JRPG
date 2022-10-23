using UnityEngine;
using VContainer.Unity;

namespace Source.Services
{
    public class TickService : ITickable
    {
        public TickService()
        {
            
        }
        
        void ITickable.Tick()
        {
            Debug.Log("abc");
        }
    }
}