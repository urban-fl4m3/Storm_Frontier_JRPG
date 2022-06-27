using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace SF.UI.Pool
{
    public class ObjectPool<TObject> where TObject : Object, IPoolable
    {
        private readonly Queue<TObject> _sleepingObjects = new Queue<TObject>();
        private readonly TObject _prefab;

        public ObjectPool(TObject prefab)
        {
            _prefab = prefab;
        }

        public TObject Get()
        {
            if(_sleepingObjects.Count == 0)
                Create();

            var button = _sleepingObjects.Dequeue();
            button.OnSpawn();
            return button;
        }

        private void Create()
        {
            var instance = Object.Instantiate(_prefab.Object);
            var poolable = instance.GetComponent<TObject>();
            poolable.ReturnToPool += OnReturnToPool;
            
            _sleepingObjects.Enqueue(poolable);

            void OnReturnToPool()
            {
                _sleepingObjects.Enqueue(poolable);
            }
        }
    }
}