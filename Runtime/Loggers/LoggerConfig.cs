using UnityEngine;

namespace Futura.ApiRequestsBuilder.Loggers
{
    internal class LoggerConfig : ScriptableObject
    {
        [SerializeField]
        private bool _enableLogging = true;
        
        [SerializeField]
        private LogLevel _logLevel = LogLevel.Info;

        public bool EnableLogging => _enableLogging;

        public LogLevel LogLevel => _logLevel;
    }
}