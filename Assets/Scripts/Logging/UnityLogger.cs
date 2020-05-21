using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ILogger = DeepWolf.Logging.ILogger;

namespace DeepWolf.Logging
{
    public class UnityLogger : ILogger
    {
        public void LogInfo(string message) => Debug.Log(message);

        public void LogWarning(string message) => Debug.LogWarning(message);

        public void LogError(string message) => Debug.LogError(message);
    }
}