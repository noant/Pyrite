using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

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
