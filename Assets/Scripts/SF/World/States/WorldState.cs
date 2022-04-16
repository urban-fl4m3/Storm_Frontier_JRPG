using SF.Common.Data;

namespace SF.Game.States
{
    public abstract class WorldState<TWorld> : GameState where TWorld : class, IWorld
    {
        protected TWorld World { get; private set; }
        
        protected WorldState(IServiceLocator serviceLocator) : base(serviceLocator)
        {
        }

        public override void Enter(IDataProvider data)
        {
            World = data?.GetData<TWorld>();

            if (World != null)
            {
                OnEnter(data);
            }
        }

        public override void Exit()
        {
            
        }
        
        protected abstract void OnEnter(IDataProvider data);
        protected abstract void OnExit();
    }
}