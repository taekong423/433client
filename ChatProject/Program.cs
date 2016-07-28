using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatProject
{
    class Program
    {
        public static string command = "";
        static void Main(string[] args)
        {
            
            ////Byte Length Test
            //while (true)
            //{
            //    string str1;
            //    str1 = Console.ReadLine();

            //    string str2;
            //    str2 = Console.ReadLine();
                
            //    Console.WriteLine("STR1 Length : " + str1.Length + "\n");
            //    Console.WriteLine("STR2 Length : " + str2.Length + "\n");

            //    byte[] byt1 = new byte[12];
            //    Encoding.UTF8.GetBytes(str1,0,str1.Length,byt1,0);//PROBLEM
            //    Console.WriteLine("byte1 : " + Encoding.UTF8.GetString(byt1,0,11) + "\n");
            //    Console.WriteLine("byte1 Length : " + byt1.Length + "\n");
                

            //    byte[] byt2 = new byte[18];
            //    Encoding.UTF8.GetBytes(str2,0,str2.Length,byt2,0);
            //    Console.WriteLine("byte2 Length : " + byt2.Length + "\n");

            //    byte[] conByte1;
            //    conByte1 = byt1.Concat(byt2).ToArray();
            //    Console.WriteLine("conByte1 Length : " + conByte1.Length + "\n");

            //    byte[] conByte2 = new byte[30];
            //    conByte2 = byt1.Concat(byt2).ToArray();
            //    Console.WriteLine("conByte2 Length : " + conByte2.Length + "\n");
            //}
            

            Console.WriteLine("Start ");                      
            while (true) {
                Console.WriteLine("Welcome !");
                Console.WriteLine("Please Enter Command\nRun Client : 1 or client\nRun Dummy Client : 2 or dummy\nRun Server : 3 or server\n0 is Quit");
                command = Console.ReadLine();
                switch (command)
                {                    
                    case "1":
                    case "Client":
                    case "client":
                        Console.WriteLine("Run Client");
                        Auth auth = new Auth();
                        command = "";                        
                        break;
                    case "2":
                    case "Dummy":
                    case "dummy":
                        Console.WriteLine("Run Dummy Client");
                        command = "";
                        break;
                    case "3":
                    case "Server":
                    case "server":
                        Console.WriteLine("Run Server");
                        command = "";
                        break;

                    case "0":
                    case "quit":
                    case "Quit":
                        Console.WriteLine("Exit");
                        return;                                                

                }
            }
        }
    }
}
