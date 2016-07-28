using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace ChatProject
{
    class CommonClient
    {
        Socket socket = null;
        string clientName;

        //CHECK: Encrypt
        //is password needed ?
        string password;

        CommonClient()
        {

        }

        public CommonClient(Socket soc, string name, string password)        
        {
            this.socket = soc;
            this.clientName = name;
            this.password = password;

            //CHECK: send to FE   my account
        }       

        void SendMsg()
        {

        }

        void RecvMsg()
        {

        }
    }
}
