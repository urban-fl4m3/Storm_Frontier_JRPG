using SF.Common.Commands;

namespace SF.Battle.Actions
{
    public abstract class BattleAction : ICommand
    {
        public abstract void Execute();
    }
}