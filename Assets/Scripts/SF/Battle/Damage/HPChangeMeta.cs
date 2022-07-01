namespace SF.Battle.Damage
{
    public readonly struct HPChangeMeta
    {
        public readonly int Amount;
        
        public HPChangeMeta(int amount)
        {
            Amount = amount;
        }
    }
}