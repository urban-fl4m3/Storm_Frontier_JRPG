namespace Source.Services.Loggers
{
    public interface IDebugLogger
    {
        void Log(string msg);
        void LogError(string msg);
        void LogWarning(string msg);
    }
}