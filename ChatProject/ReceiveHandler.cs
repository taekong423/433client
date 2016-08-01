using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Ircc.IrccHelper;

namespace Ircc
{
    class ReceiveHandler
    {
        public Packet PacketHandler(Packet recvPacket)
        {
            byte[] returnData = null;
            Header returnHeader = new Header();

            //Client to Server side
            if(Comm.CS==recvPacket.header.comm)
            {
                switch(recvPacket.header.code)
                {
                    //Received SignUp Request
                    case Code.SIGNUP:
                        //FE and BE side
                        //Do SignUp Process                        
                        break;                    
                    case Code.SIGNUP_ERR:
                        //CL side
                        //error handling
                        break;
                    case Code.SIGNUP_RES:
                        //CL side
                        //success
                        break;

                    case Code.LOGIN:
                        //FE and BE side
                        break;
                    case Code.LOGIN_ERR:
                        //CL side
                        break;
                    case Code.LOGIN_RES:
                        //CL side
                        break;
                }

            }
            //Server to Server Side
            else if(Comm.SS == recvPacket.header.comm)
            {

            }
            //Dummy to Server Side
            else if (Comm.DUMMY == recvPacket.header.comm)
            {

            }

                Packet returnPacket = new Packet(returnHeader,returnData);
            return new Packet();
        }
    }
}
