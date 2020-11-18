using System;

namespace PhasmophobiaSaveEditor.Extensions
{
    internal static class ArgumentVerificationExtensions
    {
        public static void TestNotEmptyString(this string parameter, string parameterName)
        {
            if (string.IsNullOrEmpty(parameter))
            {
                throw new ArgumentException($"The parameter '{parameterName}' should not be empty string.", parameterName);
            }
        }

        public static void TestNotNull(this object parameter, string parameterName)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(parameterName);
            }
        }
    }
}