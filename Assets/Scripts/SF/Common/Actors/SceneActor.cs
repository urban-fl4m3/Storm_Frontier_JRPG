using SF.Game;
using SF.Game.Worlds;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SF.Common.Actors
{
    [RequireComponent(typeof(ActorComponentContainer))]
    public class SceneActor : SerializedMonoBehaviour, IActor
    {
        public ActorComponentContainer Components { get; private set; }
        public IWorld World { get; private set; }

        protected IServiceLocator ServiceLocator { get; private set; }

        public virtual void Enable()
        {
            
        }

        public virtual void Disable()
        {
            
        }

        public void SetNewPlaceholder(Transform placeholder)
        {
            Components.Get<PlaceholderComponent>().Placeholder = placeholder;
            SyncWith(placeholder);
        }

        protected void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
        
        protected void LookAt(Vector3 lookAtVector)
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
        
        protected void PlaceInFrontOf(SceneActor actor)
        {
            if (actor != null)
            {
                var place = actor.Components.Get<PlaceholderComponent>().FrontPoint;
                SetPosition(place.transform.position);
                LookAt(actor.GetPosition() - place.position);
            }
        }

        private Vector3 GetPosition()
        {
            return transform.position;
        }

        private void SyncWith(Transform tr)
        {
            SetPosition(tr.position);
            LookAt(tr.forward);
        }
    }
}