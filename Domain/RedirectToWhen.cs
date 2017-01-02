﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Domain
{
    public static class RedirectToWhen
    {
        static readonly MethodInfo InternalPreserveStackTraceMethod = typeof(Exception).GetMethod("InternalPreserveStackTrace", BindingFlags.Instance | BindingFlags.NonPublic);

        private static class Cache<T>
        {
            // ReSharper disable StaticFieldInGenericType
            public static readonly IDictionary<Type, MethodInfo> Dict = typeof(T)
                .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(m => m.Name == "When")
                .Where(m => m.GetParameters().Length == 1)
                .ToDictionary(m => m.GetParameters().First().ParameterType, m => m);
            // ReSharper restore StaticFieldInGenericType
        }

        [DebuggerNonUserCode]
        public static void InvokeEvent<T>(T instance, object @event)
        {
            MethodInfo info;
            var type = @event.GetType();
            if (!Cache<T>.Dict.TryGetValue(type, out info))
            {
                // we don't care if state does not consume events
                // they are persisted anyway
                return;
            }
            try
            {
                info.Invoke(instance, new[] { @event });
            }
            catch (TargetInvocationException ex)
            {
                if (null != InternalPreserveStackTraceMethod)
                    InternalPreserveStackTraceMethod.Invoke(ex.InnerException, new object[0]);
                if (ex.InnerException != null) throw ex.InnerException;
            }
        }

        [DebuggerNonUserCode]
        public static void InvokeCommand<T>(T instance, object command)
        {
            MethodInfo info;
            var type = command.GetType();
            if (!Cache<T>.Dict.TryGetValue(type, out info))
            {
                var s = $"Failed to locate {typeof(T).Name}.When({type.Name})";
                throw new InvalidOperationException(s);
            }
            try
            {
                info.Invoke(instance, new[] { command });
            }
            catch (TargetInvocationException ex)
            {
                if (null != InternalPreserveStackTraceMethod)
                    InternalPreserveStackTraceMethod.Invoke(ex.InnerException, new object[0]);
                if (ex.InnerException != null) throw ex.InnerException;
            }
        }
    }
}