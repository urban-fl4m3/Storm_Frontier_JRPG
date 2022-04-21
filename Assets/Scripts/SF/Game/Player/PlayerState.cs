namespace SF.Game.Player
{
    public class PlayerState : IPlayerState
    {
        public BattleLoadout Loadout { get; }

        public PlayerState()
        {
            Loadout = new BattleLoadout();
        }
    }
}