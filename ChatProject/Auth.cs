using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using Ircc;
using static Ircc.IrccHelper;

namespace ChatProject
{
    class Auth
    {
        public Socket socket = null;
        string clientName;
        string password;

        //IPAddress address = IPAddress.Parse("10.100.58.5");
        IPAddress address = IPAddress.Parse("127.0.0.1");
        int portNum = 30000;


        public Auth()
        {
            Console.WriteLine("\nIt will enter the Authrority checking Process.\n Please Wait");
            Console.WriteLine("\nConnecting...");

            TryConnect();
            while(socket.IsBound)
            {
                Console.WriteLine("\nDo You Have An ID ?\n If you Have, press Y Key OR ANY.\n If not, press N Key \n (y / n) : ");

                ConsoleKeyInfo keyInfo;
                keyInfo = Console.ReadKey();

                //Sign up Process
                if (keyInfo.KeyChar == 'n' || keyInfo.KeyChar == 'N')
                {
                    Console.WriteLine("\nYou will enter the Sign Up Process.\n Please Wait for seconds");                    
                    try
                    {
                        short resultCode = SignUp();
                        //Receivec Packet Code Check
                        if (Code.SIGNUP_ERR == resultCode)//Fail
                        {
                            Console.WriteLine("\nSign up is FAILED");

                            //what is the error?
                            Console.WriteLine("\nReturn to the First Command window");
                        }
                        else if (Code.SIGNUP_RES == resultCode)//Success
                        {
                            Console.WriteLine("\nSignUp Success");
                            CommonClient client = new CommonClient(socket, clientName);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\nWrong Type Message is received (Unexpected Message)");
                        }
                    }
                    catch(SocketException se)
                    {
                        Console.WriteLine("\nSocketException : {0}", se.Message);

                        //CHECK: close socket
                        socket.Close();
                        Console.WriteLine("\nReturn to the First Page");                        
                        break;
                    }
                    catch(ArgumentException ae)
                    {
                        Console.WriteLine("\nArgumentException : {0} \nName(4~12 character)\npassword(6~18)", ae.Message);                                                
                    }
                    catch(Exception e)
                    {
                        //CHECK: Exception Handling
                        Console.WriteLine("\nException : {0}",e.Message);
                        break;
                    }                    
                }
                else//Login Process
                {                    
                    try
                    {
                        short resultCode = Login();

                        //Receivec Packet Code Check
                        if (Code.LOGIN_ERR == resultCode)//Fail
                        {
                            Console.WriteLine("\nLogin is FAILED");

                            //what is the error?
                            Console.WriteLine("\nReturn to the First Command window");
                        }
                        else if (Code.LOGIN_RES == resultCode)//Success
                        {
                            Console.WriteLine("\nLogin Success");
                            CommonClient client = new CommonClient(socket, clientName);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\nWrong Type Message is received (Unexpected Message)");
                        }
                    }
                    catch (SocketException se)
                    {
                        Console.WriteLine("\nSocketException : {0}", se.Message);

                        //CHECK: close socket
                        socket.Close();
                        Console.WriteLine("\nReturn to the First Page");
                        break;
                    }
                    catch (ArgumentException ae)
                    {
                        Console.WriteLine("\nArgumentException : {0} \nName(4~12 character)\npassword(6~18)", ae.Message);
                    }
                    catch (Exception e)
                    {
                        //CHECK: Exception Handling
                        Console.WriteLine("\nException : {0}", e.Message);
                        break;
                    }                   
                }
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

        short Login()
        {
            Console.WriteLine("\n\n----------Login Process----------");
            
            Console.WriteLine("Name (ID) : ");
            clientName = Console.ReadLine();            
           
            Console.WriteLine("Password : ");
            password = Console.ReadLine();

            if (4 > clientName.Length || 6 > password.Length)
            {
                throw new ArgumentException();
            }
            
            //Packet Parsing
            //send ID and Password            
            socket.Send(CreateBytesPacket(Comm.CS,Code.LOGIN, LoginStrToByte(clientName, password)));            

            //Receive Validation
            //Packet Parsing
            //check result
            Packet resPacket;
            Console.WriteLine("\nWait for Response Message");
            resPacket = RecvMsg();                       
                        
            return resPacket.header.code;
        }

        short SignUp()
        {
            Console.WriteLine("\n\n----------Sign Up Process----------");
            
            Console.WriteLine("Name (ID) : ");
            clientName = Console.ReadLine();

            Console.WriteLine("Password : ");
            password = Console.ReadLine();

            if (4 > clientName.Length || 6 > password.Length)
            {
                throw new ArgumentException();
            }
            
            //Packet Parsing
            //send ID and Password           
            socket.Send(CreateBytesPacket(Comm.CS, Code.SIGNUP, LoginStrToByte(clientName, password)));            

            //Receive Validation
            //Packet Parsing
            //check result
            Packet resPacket;
            Console.WriteLine("\nWait for Response Message");
            resPacket = RecvMsg();

            return resPacket.header.code;
        }

        //convert string to byte for login
        //It fixed byte array [30]
        byte[] LoginStrToByte(string name, string pass)
        {
            byte[] bClientName = new byte[12];
            byte[] bPassword = new byte[18];

            //parameter : string, startIndex, lastIndex, stored byte[], byte[] start index
            Encoding.UTF8.GetBytes(name, 0, name.Length, bClientName, 0);
            Encoding.UTF8.GetBytes(pass, 0, pass.Length, bPassword, 0);

            byte[] bMsg = bClientName.Concat(bPassword).ToArray();

            return bMsg;
        }

        Packet RecvMsg()
        {
            //CHECK: set Timer or not

            //CHECK: variable name
            Header recvdHeader;
            Packet recvdPacket;
            int bytecount;

            //get HEADER
            byte[] header = new byte[HEADER_SIZE];
            bytecount = socket.Receive(header);
            recvdHeader = bytesToHeader(header);
            recvdPacket.header = recvdHeader;

            // get DATA
            byte[] data = new byte[recvdHeader.size];
            bytecount = socket.Receive(data);
            recvdPacket.data = data;

            return recvdPacket;
        }
    }
}
