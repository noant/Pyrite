using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniActionsCore
{
    public class Result<T>
    {
        private List<Exception> _exceptions;
        public IEnumerable<Exception> Exceptions { 
            get {
                if (_exceptions == null)
                    _exceptions = new List<Exception>();
                return _exceptions;
            }
        }
        public T Value { get; internal set; }

        internal void AddException(Exception e)
        {
            if (_exceptions == null)
                _exceptions = new List<Exception>();

            _exceptions.Add(e);
        }

        internal void AddExceptions(IEnumerable<Exception> es)
        {
            foreach (var e in es)
                AddException(e);
        }
    }

    public class VoidResult : Result<object>
    { 
        
    }
}
