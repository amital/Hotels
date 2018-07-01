using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace Payoneer.Payoneer.Hotels.WebApi
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    [ExcludeFromCodeCoverage]
    internal static class ConfigReader
    {
        public static TValue GetValue<TValue>(
            string key, TryParseDelegate<TValue> tryParse, TValue defaultValue = default(TValue))
        {
            return Parse(ConfigurationManager.AppSettings[key], tryParse, defaultValue);
        }

        public static TValue Parse<TValue>(
            string text, TryParseDelegate<TValue> tryParse, TValue defaultValue = default(TValue))
        {
            return tryParse(text, out TValue value) ? value : defaultValue;
        }

        public delegate bool TryParseDelegate<TValue>(string text, out TValue value);
    }
}