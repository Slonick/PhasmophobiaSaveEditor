using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace PhasmophobiaSaveEditor.Utils
{
    public static class StackTraceUtils
    {
        private static readonly Assembly MsCorLibAssembly = typeof(string).Assembly;
        private static readonly Assembly SystemAssembly = typeof(Debug).Assembly;

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetClassFullName()
        {
            const int skipFrames = 2;
            return GetClassFullName(new StackFrame(skipFrames, false));
        }

        public static string GetClassFullName(StackFrame stackFrame)
        {
            var text = LookupClassNameFromStackFrame(stackFrame);
            if (string.IsNullOrEmpty(text))
            {
                text = GetClassFullName(new StackTrace(false));
            }

            return text;
        }

        public static string GetStackFrameMethodClassName(MethodBase method, bool includeNameSpace, bool cleanAsyncMoveNext, bool cleanAnonymousDelegates)
        {
            if (method == null)
            {
                return null;
            }

            var declaringType = method.DeclaringType;
            if (cleanAsyncMoveNext && method.Name == "MoveNext" && (declaringType != null ? declaringType.DeclaringType : null) != null && declaringType.Name.StartsWith("<") && declaringType.Name.IndexOf('>', 1) > 1)
            {
                declaringType = declaringType.DeclaringType;
            }

            if (!includeNameSpace && (declaringType != null ? declaringType.DeclaringType : null) != null && declaringType.IsNested && declaringType.GetCustomAttribute<CompilerGeneratedAttribute>() != null)
            {
                return declaringType.DeclaringType?.Name;
            }

            var text = includeNameSpace ? declaringType != null ? declaringType.FullName : null : declaringType != null ? declaringType.Name : null;
            if (cleanAnonymousDelegates && text != null)
            {
                var num = text.IndexOf("+<>", StringComparison.Ordinal);
                if (num >= 0)
                {
                    text = text.Substring(0, num);
                }
            }

            return text;
        }

        public static Assembly LookupAssemblyFromStackFrame(StackFrame stackFrame)
        {
            var method = stackFrame.GetMethod();
            if (method == null)
            {
                return null;
            }

            var declaringType = method.DeclaringType;
            Assembly assembly;
            if ((assembly = declaringType != null ? declaringType.Assembly : null) == null)
            {
                var module = method.Module;
                assembly = module.Assembly;
            }

            var assembly2 = assembly;
            if (assembly2 == MsCorLibAssembly)
            {
                return null;
            }

            if (assembly2 == SystemAssembly)
            {
                return null;
            }

            return assembly2;
        }

        public static string LookupClassNameFromStackFrame(StackFrame stackFrame)
        {
            var method = stackFrame.GetMethod();
            if (method != null && LookupAssemblyFromStackFrame(stackFrame) != null)
            {
                var text = GetStackFrameMethodClassName(method, true, true, true) ?? method.Name;
                if (!string.IsNullOrEmpty(text) && !text.StartsWith("System.", StringComparison.Ordinal))
                {
                    return text;
                }
            }

            return string.Empty;
        }

        private static string GetClassFullName(StackTrace stackTrace)
        {
            var frames = stackTrace.GetFrames();
            if (frames == null)
            {
                return string.Empty;
            }

            foreach (var t in frames)
            {
                var text = LookupClassNameFromStackFrame(t);
                if (!string.IsNullOrEmpty(text))
                {
                    return text;
                }
            }

            return string.Empty;
        }
    }
}