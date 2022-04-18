namespace SF.Game.Extensions
{
    public static class TeamExtensions
    {
        public static Team GetOppositeTeam(this Team team)
        {
            return team == Team.Enemy ? Team.Player : Team.Enemy;
        }
    }
}