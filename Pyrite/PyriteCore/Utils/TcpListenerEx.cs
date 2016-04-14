using System.Net.Sockets;

namespace PyriteCore
{
    public class TcpListenerEx : TcpListener
    {
#pragma warning disable CS0618 // Type or member is obsolete
        public TcpListenerEx(int port) : base(port)
        {
            this.Server.ReceiveTimeout =
                this.Server.SendTimeout = 500;
        }
#pragma warning restore CS0618 // Type or member is obsolete

        public bool IsActive
        {
            get
            {
                return base.Active;
            }
        }
    }
}
