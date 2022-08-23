using SF.Game;
using Sirenix.OdinInspector;
using UniRx;

namespace SF.Common.Actors
{
    public abstract class ActorComponent : SerializedMonoBehaviour, IActorComponent
    {
        public IReadOnlyReactiveProperty<bool> IsInit => _isInit;
        
        protected IActor Owner { get; private set; }
        protected IServiceLocator ServiceLocator { get; private set; }

        private readonly ReactiveProperty<bool> _isInit = new();
        
        public void Init(IActor owner, IServiceLocator serviceLocator)
        {
            Owner = owner;
            ServiceLocator = serviceLocator;
            
            OnInit();
        }

        protected virtual void OnInit()
        {
            _isInit.Value = true;
        }
    }
}