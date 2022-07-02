namespace SF.Battle.Actors
{
    public interface ITurnConsumer : ITurnPasser
    {
        void EndTurn();
    }
}