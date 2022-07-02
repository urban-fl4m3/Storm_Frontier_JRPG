namespace SF.Battle.Abilities.Mechanics.Data
{
    public interface IEffectData
    {
        bool IsStackable { get; }
        bool HasDuration { get; }
        int Rounds { get; }
    }
}