namespace Test.Runtime
{
    public class PlayerDebugContainer: IDeBufProvider
    {
        public PlayerDeBufStatus GetDeBuf()
        {
            return new PlayerDeBufStatus();
        }

        public void NextTurn()
        {
            
        }
    }
}