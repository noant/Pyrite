using System.Net.Sockets;

namespace UniActionsCore
{
    public class TcpListenerEx : TcpListener
    {
        public TcpListenerEx(int port) : base(port) { }

        public bool IsActive
        {
            get
            {
                return base.Active;
            }
        }
    }
}
