namespace Test.Runtime
{
    public abstract class DeBufDecorator : IDeBufProvider
    {
        protected readonly IDeBufProvider WrappedEntity;
        protected int StatusCapacity;

        protected DeBufDecorator(IDeBufProvider wrappedEntity, int statusCapacity)
        {
            WrappedEntity = wrappedEntity;
            StatusCapacity = statusCapacity;
        }

        public PlayerDeBufStatus GetDeBuf()
        {
            return GetDeBugInternal();
        }

        public void NextTurn()
        {
            SetNextTurn();
        }

        protected abstract PlayerDeBufStatus GetDeBugInternal();
        protected abstract void SetNextTurn();
    }
}