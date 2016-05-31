using System;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            var pingChecker = new PingChecker.PingChecker();
            
            while (true)
            {
                pingChecker.BeginUserSettings();
                Console.WriteLine(pingChecker.IsCanDoNow);
            }
        }
    }
}
