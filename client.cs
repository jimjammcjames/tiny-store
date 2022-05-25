// Hello World! program
using System;
 
namespace HelloWorld
{
    class Hello {         
        static void Main(string[] args)
        {

            Console.WriteLine("Starting client");
            Console.WriteLine("Enter your username:");
            var username = Console.ReadLine();
            
            var socket = new System.Net.Sockets.TcpClient(System.Net.IPAddress.Loopback.ToString(), 8080);
            var stream = socket.GetStream();
            while (true){
                Console.WriteLine("Enter message");
                var message = Console.ReadLine();
                var writer = new System.IO.StreamWriter(stream);
                writer.WriteLine(message);
                writer.Flush();
                  
                System.Byte[] bytes = new System.Byte[2048];
                int size = stream.Read(bytes, 0, bytes.Length);
                var askii = new System.Text.ASCIIEncoding();
                string response = askii.GetString(bytes, 0, size-2);
                Console.WriteLine(response);
            }
        }
    }
}
