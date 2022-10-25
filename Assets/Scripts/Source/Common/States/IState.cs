using Cysharp.Threading.Tasks;
using Source.Common.Data;

namespace Source.Common.States
{
    public interface IState
    {
        UniTaskVoid Enter(IDataProvider dataProvider = null);
        void Exit();
    }
}