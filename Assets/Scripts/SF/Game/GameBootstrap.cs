using UnityEngine;

namespace SF.Game
{
    public class GameBootstrap : MonoBehaviour
    {
        private IServiceLocator _serviceLocator;
        private IWorld _world;
        
        private void Start()
        {
            _serviceLocator = new ServiceLocator();
            _world = new DefaultWorld(_serviceLocator);
        }
    }
}