namespace Hamurabi.Core
{
    public enum TurnHandleResult
    {
        ValidationError,
        Succeed,
        GameOver
    }


    public enum GameOverCause
    {
        None,
        PeopleDead,
        CameLastYear
    }
}