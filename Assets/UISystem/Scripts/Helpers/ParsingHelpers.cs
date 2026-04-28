using System.Text.RegularExpressions;
using UnityEngine;

namespace UISystem.Helpers
{
    /// <summary>
    /// Helper class to parse.
    /// </summary>
    internal static class ParsingHelpers
    {
        /// <summary>
        /// Tries parsing string to Vector2Int.
        /// </summary>
        /// <param name="value">Input string.</param>
        /// <param name="result">Output Vector2Int result.</param>
        /// <returns>True if could parse string, false otherwise.</returns>
        public static bool TryParseVector2Int(string value, out Vector2Int result)
        {
            result = Vector2Int.zero;
            var match = Regex.Match(value, @"(-?\d+\.?\d*)\D+(-?\d+\.?\d*)");

            if (match.Success && match.Groups.Count == 2)
            {
                int x = int.Parse(match.Groups[1].Value);
                int y = int.Parse(match.Groups[2].Value);
                result = new Vector2Int(x, y);
                return true;
            }

            return false;
        }
    }
}
