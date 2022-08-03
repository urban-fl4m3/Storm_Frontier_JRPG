using SF.Game;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SF.Common.Actors
{
    [RequireComponent(typeof(ActorComponentContainer))]
    public class SceneActor : SerializedMonoBehaviour, IActor
    {
        [SerializeField] private GameObject _view;
        
        public ActorComponentContainer Components { get; private set; }
        public IWorld World { get; private set; }

        protected IServiceLocator ServiceLocator { get; private set; }

        public virtual void Enable()
        {
            
        }

        public virtual void Disable()
        {
            
        }
        
        public void SetVisibility(bool isVisible)
        {
            _view.gameObject.SetActive(isVisible);
        }
        
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }
        
        public void LookAt(Vector3 lookAtVector)
        {
            transform.rotation = Quaternion.LookRotation(lookAtVector, transform.up);
        }
        
        protected void Init(IServiceLocator serviceLocator, IWorld world)
        {
            World = world;
            ServiceLocator = serviceLocator;
            
            Components = GetComponent<ActorComponentContainer>();
            Components.InitActorComponents(serviceLocator);
        }
    }
}