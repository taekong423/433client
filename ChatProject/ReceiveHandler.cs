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
                    //------------CREATE------------
                    case Code.CREATE:
                        //FE side
                        break;
                    case Code.CREATE_DUPLICATE_ERR:
                        //CL side
                        break;
                    case Code.CREATE_FULL_ERR:
                        //CL side
                        break;


                    //------------DESTROY------------
                    case Code.DESTROY:
                        //FE side
                        break;
                    case Code.DESTROY_ERR:
                        //CL side
                        break;                        


                    //------------FAIL------------
                    case Code.FAIL:
                        //FE side
                        break;


                    //------------HEARTBEAT------------
                    case Code.HEARTBEAT:
                        //CL side
                        break;
                    case Code.HEARTBEAT_RES:
                        //FE side
                        break;


                    //------------JOIN------------
                    case Code.JOIN:
                        //FE side
                        break;
                    case Code.JOIN_FULL_ERR:
                        //CL side
                        break;
                    case Code.JOIN_NULL_ERR:
                        //CL side
                        break;                        


                    //------------LEAVE------------
                    case Code.LEAVE:
                        //FE side
                        break;
                    case Code.LEAVE_ERR:
                        //CL side
                        break;


                    //------------LIST------------
                    case Code.LIST:
                        //FE side
                        break;
                    case Code.LIST_ERR:
                        //CL side
                        break;
                    case Code.LIST_RES:
                        //CL side
                        break;                    


                    //------------MSG------------
                    case Code.MSG:
                        //CL and FE side
                        break;
                    case Code.MSG_ERR:
                        //CL and FE side
                        break;


                    //------------SIGNIN------------
                    case Code.SIGNIN:
                        //FE and BE side
                        break;
                    case Code.SIGNIN_ERR:
                        //CL side
                        break;
                    case Code.SIGNIN_RES:
                        //CL side
                        break;


                    //------------SIGNUP------------
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


                    //------------SUCCESS------------
                    case Code.SUCCESS:
                        //FE side
                        break;                    
                }

            }
            //Server to Server Side
            else if(Comm.SS == recvPacket.header.comm)
            {
                switch (recvPacket.header.code)
                {
                    //------------SDESTROY------------
                    case Code.SDESTROY:
                        //FE side
                        break;
                    case Code.SDESTROY_ERR:
                        //FE side
                        break;


                    //------------SJOIN------------
                    case Code.SJOIN:
                        //FE side
                        break;
                    case Code.SJOIN_ERR:
                        //FE side
                        break;


                    //------------SLIST------------
                    case Code.SLIST:
                        //FE side
                        break;
                    case Code.SLIST_ERR:
                        //FE side
                        break;
                    case Code.SLIST_RES:
                        //FE side
                        break;


                    //------------SMSG------------                
                    case Code.SMSG:
                        //FE side
                        break;
                    case Code.SMSG_ERR:
                        //FE side
                        break;
                }
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
