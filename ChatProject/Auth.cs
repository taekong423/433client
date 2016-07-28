using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace ChatProject
{
    class Auth
    {
        public Socket socket = null;
        string clientName;
        string password;
       
        int portNum = 4334;  

        public Auth()
        {
            Console.WriteLine("\nIt will enter the Authrority checking Process.\n Please Wait");
            Console.WriteLine("\nConnecting...");

            TryConnect();
            if(socket.IsBound)
            {
                Console.WriteLine("\nDo You Have An ID ?\n If you Have, press Y Key OR ANY.\n If not, press N Key \n (y / n) : ");

                ConsoleKeyInfo keyInfo;
                keyInfo = Console.ReadKey();
                if (keyInfo.KeyChar == 'n' || keyInfo.KeyChar == 'N')
                {
                    Console.WriteLine("\nYou will enter the Sign Up Process.\n Please Wait for seconds");

                    if (0 == SignUp())//Fail
                    {
                        Console.WriteLine("\nFAIL");
                    }
                    else//Success
                    {
                        Console.WriteLine("\nSignUp");
                        CommonClient client = new CommonClient(socket, clientName, password);
                    }
                }
                else
                {
                    //CHECK: integer                    
                    if (0 == Login())//Fail
                    {
                        Console.WriteLine("\nFAIL");
                    }
                    else//Success
                    {
                        Console.WriteLine("\nLogin");
                    }
                }
            }
            else
            {
                //exit
                Console.WriteLine("\nReturn to the First Command window");
            }          
        }

        void TryConnect()
        {
            try
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                while (!TryBind())
                {
                    //retry?
                    Console.WriteLine("\nIf you want to retry, press any key.\nbut If you want to quit, press 'q'");

                    ConsoleKeyInfo cki;
                    cki = Console.ReadKey();
                    if (cki.KeyChar == 'q' || cki.KeyChar == 'Q')
                    {
                        Console.WriteLine("\nQuit this program");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Retry...\n");
                    }
                }
            }
            catch (SocketException se)
            {
                Console.WriteLine(se);
                Console.WriteLine("Error Code : " + se.ErrorCode + "\n");
            }
            finally
            {
                //CHECK:
                if (!socket.IsBound)
                {
                    //CHECK: shutdown
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
            }
        }

        bool TryBind()
        {
            try
            {
                //socket.Connect(new IPEndPoint(IPAddress.Loopback, portNum));
                IPAddress address = IPAddress.Parse("10.100.58.5");
                portNum = 30000;

                socket.Connect(new IPEndPoint(address, portNum));
                Console.WriteLine("Connected");
            }
            catch (SocketException se)
            {
                if (socket.IsBound)
                    Console.WriteLine("Socket isn't bound\n connection refused");
                Console.WriteLine("unable because : Error code " + se.ErrorCode + "\n\n");
                //ErrorCode ==10049 : PortNumber
                if (se.ErrorCode == 10061)
                {
                    Console.WriteLine("Connection refused\ninactive on the foreign host. no server application running.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("unable because : \n" + e + "\n\n");
            }

            return socket.IsBound;
        }

        int Login()
        {
            int result = 0;

            //CHECK: valid format
            Console.WriteLine("Name (ID) : ");
            clientName = Console.ReadLine();            
           
            Console.WriteLine("Password : ");
            password = Console.ReadLine();
            
            
            //Packet Parsing
            //send ID and Password
            Console.WriteLine("\nSend strat");        
            SendLoginMsg(LoginStrToByte(clientName,password));
            Console.WriteLine("\nSend ended");

            //Receive Validation
            //Packet Parsing
            //check result

            result = 100; // success
            return result;
        }

        int SignUp()
        {
            int result = 0;

            Console.WriteLine("\n----------Sign Up Process----------");

            Console.WriteLine("Name (ID) : ");

            Console.WriteLine("Password : ");

            //CHECK:
            //Packet Parsing
            //send ID and Password


            //Packet Parsing
            //check result

            result = 100; //success
            return result;
        }

        //convert string to byte for login
        //It fixed byte array [30]
        byte[] LoginStrToByte(string name, string pass)
        {
            byte[] bClientName = new byte[12];
            byte[] bPassword = new byte[18];

            Encoding.UTF8.GetBytes(name, 0, name.Length, bClientName, 0);
            Encoding.UTF8.GetBytes(pass, 0, pass.Length, bPassword, 0);

            byte[] bmsg = bClientName.Concat(bPassword).ToArray();

            return bmsg;
        }

       

        void SendLoginMsg(byte[] bMsg)
        {
            //CHECK: have to functionalize
            Header header;
            header.comm = 0;
            header.code = 302;
            header.size = bMsg.Length;
            header.reserved = 0;

            Packet packet;
            packet.header = header;
            packet.data = bMsg;            

            //send start
            socket.Send(IrccHelper.packetToBytes(packet));

        }

        void RecvMsg()
        {
            //CHECK:
        }
    }
}
