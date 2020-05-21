namespace DeepWolf.Logging
{
    /// <summary>
    /// Represents a logger.
    /// </summary>
    public interface ILogger
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message);
    }
}