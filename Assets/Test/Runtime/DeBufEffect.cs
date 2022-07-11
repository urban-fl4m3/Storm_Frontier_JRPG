using UnityEngine;

namespace Test.Runtime
{
    public class DeBufEffect: DeBufDecorator
    {
        private readonly DeBufTypes _deBufTypes;
        public DeBufEffect(IDeBufProvider wrappedEntity, int statusCapacity, DeBufTypes deBufTypes) : base(wrappedEntity, statusCapacity)
        {
            _deBufTypes = deBufTypes;
        }

        protected override PlayerDeBufStatus GetDeBugInternal()
        {
            return StatusCapacity > 0 ? WrappedEntity.GetDeBuf().AddDeBuf(_deBufTypes) : WrappedEntity.GetDeBuf();
        }

        protected override void SetNextTurn()
        {
            WrappedEntity.NextTurn();
            StatusCapacity -= 1;
        }
    }
}