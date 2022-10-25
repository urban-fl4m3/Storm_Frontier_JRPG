using UnityEngine;

namespace Source.Services.Loggers
{
    public class DebugLogger : IDebugLogger
    {
        public void Log(string msg)
        {
            Debug.Log(msg);
        }

        public void LogError(string msg)
        {
            Debug.LogError(msg);
        }

        public void LogWarning(string msg)
        {
            Debug.LogWarning(msg);
        }
    }
}