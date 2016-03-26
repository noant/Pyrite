using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Logging
{
    public static class Log
    {
        static Log()
        {
            _path = Path.Combine(Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).FullName, "Log.txt");
        }

        private static string _path;

        private static object _locker = new object();

        public static void Write(Exception e,
        [CallerMemberName] string memberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0)
        {
            lock (_locker)
                File.AppendAllText(_path, String.Format("\r\n{0} --- Member Name = {1}; Source File = {2}; Line= {3};\r\n{4};\r\n{5}", DateTime.Now, memberName, sourceFilePath, sourceLineNumber, e.Message, e.StackTrace));
        }

        public static void Write(string message)
        {
            lock (_locker)
                File.AppendAllText(_path, "\r\n" + DateTime.Now.ToString() + " -- Message -- " + message);
        }
    }
}
