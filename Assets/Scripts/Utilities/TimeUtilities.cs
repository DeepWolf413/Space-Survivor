using UnityEngine;

namespace DeepWolf.SpaceSurvivor.Utilities
{
    public static class TimeUtilities
    {
        private static readonly string defaultTimeFormat = "{0}m {1}s";
        
        /// <summary>
        /// Returns the minutes from <paramref name="totalSeconds"/>.
        /// </summary>
        /// <param name="totalSeconds">The total amount of seconds.</param>
        /// <returns>An <see cref="int"/> that indicates the amount minutes from <paramref name="totalSeconds"/>.</returns>
        public static int GetMinutes(float totalSeconds) => Mathf.FloorToInt(totalSeconds / 60);
        
        /// <summary>
        /// Returns the remaining seconds of a minute from <paramref name="totalSeconds"/>.
        /// </summary>
        /// <param name="totalSeconds">The total amount of seconds.</param>
        /// <returns>An <see cref="int"/> that indicates the amount seconds of a minute from <paramref name="totalSeconds"/>.</returns>
        public static int GetSeconds(float totalSeconds) => Mathf.FloorToInt(totalSeconds % 60);

        /// <summary>
        /// Returns a string of the time in the default time format, example: 04m 35s.
        /// </summary>
        /// <param name="totalSeconds">The total amount of seconds.</param>
        /// <returns>A string of the time in the default time format, example: 04m 35s.</returns>
        public static string GetFormattedTime(float totalSeconds) => string.Format(defaultTimeFormat, GetMinutes(totalSeconds).ToString("D2"), GetSeconds(totalSeconds).ToString("D2"));
        
        /// <summary>
        /// Returns a string of the time in the given <paramref name="format"/>.
        /// </summary>
        /// <param name="totalSeconds">The total amount of seconds.</param>
        /// <param name="format">The format of the time string. There's 2 parameters usable which is minutes{0}, and seconds{1}.</param>
        /// <returns>A string of the time in the given <paramref name="format"/>.</returns>
        public static string GetFormattedTime(float totalSeconds, string format) => string.Format(format, GetMinutes(totalSeconds).ToString("D2"), GetSeconds(totalSeconds).ToString("D2"));
    }
}