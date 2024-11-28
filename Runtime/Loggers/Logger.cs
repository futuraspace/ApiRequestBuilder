using UnityEngine;

namespace Futura.ApiRequestsBuilder.Loggers
{
    internal static class Logger
    {
        private static readonly LoggerConfig _config = Resources.Load<LoggerConfig>("LoggerConfig");

        public static void LogError(string message)
        {
            if (!_config.EnableLogging || _config.LogLevel < LogLevel.Error)
            {
                return;
            }

            Debug.LogError($"[ERROR]: {message}");
        }

        public static void LogWarning(string message)
        {
            if (!_config.EnableLogging || _config.LogLevel < LogLevel.Warning)
            {
                return;
            }

            Debug.LogWarning($"[WARNING]: {message}");
        }

        public static void LogInfo(string message)
        {
            if (!_config.EnableLogging || _config.LogLevel < LogLevel.Info)
            {
                return;
            }

            Debug.Log($"[INFO]: {message}");
        }

        public static void LogDebug(string message)
        {
            if (!_config.EnableLogging || _config.LogLevel < LogLevel.Debug)
            {
                return;
            }

            Debug.Log($"[DEBUG]: {message}");
        }
    }
}