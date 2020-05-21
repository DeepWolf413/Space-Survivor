using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeepWolf.Logging
{
    public static class Logger
    {
        /// <summary>
        /// The current <see cref="ILogger"/> being used.
        /// </summary>
        private static ILogger currentLogger;

        /// <summary>
        /// Gets or sets whether the logger should log or not.
        /// </summary>
        public static bool UseLogging { get; set; } = true;

        /// <summary>
        /// Sets the <see cref="currentLogger"/> to the specified <see cref="ILogger"/> type.
        /// </summary>
        /// <typeparam name="T">The type of the <see cref="ILogger"/> to use.</typeparam>
        public static void SetLogger<T>() where T : ILogger, new() => currentLogger = new T();

        /// <summary>
        /// Calls the <see cref="currentLogger"/>'s <see cref="ILogger.LogInfo"/> method.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public static void LogInfo(string message)
        {
            if (!UseLogging)
            { return; }
            
            if (currentLogger == null)
            { CreateDefaultLogger(); }
            
            currentLogger.LogInfo(message);
        }
        
        /// <summary>
        /// Calls the <see cref="currentLogger"/>'s <see cref="ILogger.LogWarning"/> method.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public static void LogWarning(string message)
        {
            if (!UseLogging)
            { return; }
            
            if (currentLogger == null)
            { CreateDefaultLogger(); }
            
            currentLogger.LogWarning(message);
        }
        
        /// <summary>
        /// Calls the <see cref="currentLogger"/>'s <see cref="ILogger.LogError"/> method.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public static void LogError(string message)
        {
            if (!UseLogging)
            { return; }
            
            if (currentLogger == null)
            { CreateDefaultLogger(); }
            
            currentLogger.LogError(message);
        }

        /// <summary>
        /// Creates and sets the <see cref="currentLogger"/> to the default <see cref="ILogger"/> of type <see cref="UnityLogger"/>.
        /// </summary>
        private static void CreateDefaultLogger() => SetLogger<UnityLogger>();
    }
}