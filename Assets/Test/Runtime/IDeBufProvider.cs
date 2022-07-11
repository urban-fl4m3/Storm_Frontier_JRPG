using Test.Runtime;

public interface IDeBufProvider
{
    PlayerDeBufStatus GetDeBuf();
    void NextTurn();
}
