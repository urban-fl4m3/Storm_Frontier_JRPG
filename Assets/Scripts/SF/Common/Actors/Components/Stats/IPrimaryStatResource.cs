using UniRx;

namespace SF.Common.Actors.Components.Stats
{
    public interface IPrimaryStatResource
    {
        IReadOnlyReactiveProperty<int> Max { get; }
        IReadOnlyReactiveProperty<int> Current { get; }

        void Add(int amount);
        void Remove(int amount);
    }
}