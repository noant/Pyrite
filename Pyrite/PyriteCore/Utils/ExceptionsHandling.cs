using Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PyriteCore
{
    public static class Resulting
    {
        static Resulting()
        {
            EnableExceptionHandling = true;
            NotCriticalExceptions = new List<Type>();
            CriticalHandler += (exceptions) =>
            {
                if (EnableExceptionHandling)
                    if (exceptions.Count(x => NotCriticalExceptions.Any(z => z.Equals(x.GetType()))) != exceptions.Count())
                        if (NeedShutdown != null)
                            NeedShutdown();
            };
        }

        public static List<Type> NotCriticalExceptions { get; private set; }

        public static event CriticalHandler CriticalHandler;
        public static event WarningHandler WarningHandler;

        internal static void RaiseCriticalHandler(IEnumerable<Exception> exceptions)
        {
            if (EnableExceptionHandling)
                if (CriticalHandler != null)
                    CriticalHandler(exceptions);
        }

        internal static void RaiseWarningsHandler(IEnumerable<Warning> warnings)
        {
            if (WarningHandler != null)
                WarningHandler(warnings);
        }

        public static bool EnableExceptionHandling { get; set; }

        public static event Action NeedShutdown;
    }

    public class Result<T>
    {
        private List<Exception> _exceptions;
        public IEnumerable<Exception> Exceptions
        {
            get
            {
                if (_exceptions == null)
                    _exceptions = new List<Exception>();
                return _exceptions;
            }
        }
        private List<Warning> _warnings;
        public IEnumerable<Warning> Warnings
        {
            get
            {
                if (_warnings == null)
                    _warnings = new List<Warning>();
                return _warnings;
            }
        }
        public T Value { get; internal set; }

        internal void AddException(Exception e)
        {
            Log.Write(e);

#if !DEBUG

            if (_exceptions == null)
                _exceptions = new List<Exception>();

            _exceptions.Add(e);

            if (!Resulting.NotCriticalExceptions.Any(x => x.Equals(e.GetType())))
            {
                Resulting.RaiseCriticalHandler(this.Exceptions);
            }
#endif
#if DEBUG
            throw e;
#endif
        }
        internal void AddExceptions(IEnumerable<Exception> es)
        {
            foreach (var e in es)
                AddException(e);
        }

        internal void AddWarning(Warning w, bool raiseWarnHandler)
        {
            if (_warnings == null)
                _warnings = new List<Warning>();

            _warnings.Add(w);

            if (raiseWarnHandler)
                Resulting.RaiseWarningsHandler(this.Warnings);
        }

        internal void AddWarning(Warning w)
        {
            AddWarning(w, false);
        }

        internal void AddWarnings(IEnumerable<Warning> ws)
        {
            foreach (var e in ws)
                AddWarning(e);
        }
    }

    public class Warning : Exception
    {
        public Warning(string message) : base(message) { }
    }

    public class VoidResult : Result<object>
    {

    }

    public delegate void CriticalHandler(IEnumerable<Exception> exceptions);
    public delegate void WarningHandler(IEnumerable<Warning> warns);
}
