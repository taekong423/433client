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
        //how about token?
        string password;

        CommonClient()
        {

        }

        public CommonClient(Socket soc, string name)        
        {
            this.socket = soc;
            this.clientName = name;

            //CHECK: send to FE   my account
        }       

        void CreatRoom()
        {

        }



        void SendMsg()
        {

        }

        void RecvMsg()
        {

        }
    }
}
