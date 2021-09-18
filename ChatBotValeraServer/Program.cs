using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotValeraServer
{
    class Program
    {
        static int port = 1337; // порт сервера
        //static string address = "127.0.0.1"; // адрес сервера
        static void Main(string[] args)
        {
            ChatContext chatContext = new ChatContext();
            // получаем адреса для запуска сокета
            IPAddress iPAddress = IPAddress.Parse("127.0.0.1"); //localhost
            IPEndPoint ipPoint = new IPEndPoint(iPAddress, port);

            // об'єкт для отримання адреси відправника
            EndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

            // создаем сокет
            Socket listenSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            try
            {
                Console.WriteLine(chatContext.Answers);
                // связываем сокет с локальной точкой, по которой будем принимать данные
                listenSocket.Bind(ipPoint);
                Console.WriteLine("Server started! Waiting for connection...");

                while (true)
                {
                    // получаем сообщение
                    int bytes = 0;
                    byte[] data = new byte[1024];
                    bytes = listenSocket.ReceiveFrom(data, ref remoteEndPoint);

                    string msg = Encoding.Unicode.GetString(data, 0, bytes);
                    Console.WriteLine($"{DateTime.Now.ToShortTimeString()}: {msg} from {remoteEndPoint}");
                    // Send data to user
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                    
                    if (chatContext.Messages.Where((el)=> el.MessageText == msg).Count() == 0)
                    {

                        byte[] data2 = Encoding.Unicode.GetBytes("Haven`t answer");

                        socket.SendTo(data, remoteEndPoint);
                    }
                    else
                    {
                        ICollection<Answer> answers = chatContext.Messages.Where((el) => el.MessageText == msg).First().Answers;
                        Random rnd = new Random();

                        //chatContext.Messages.Where((el) => el.)
                        byte[] data2 = Encoding.Unicode.GetBytes($"{answers.ToArray()[rnd.Next(0, answers.Count)]}");
                        socket.SendTo(data, remoteEndPoint);

                    }
                    //chatContext.Answers.Where((el)=>el.).

                    // отправляем ответ
                    string message = "Message was send!";
                    data = Encoding.Unicode.GetBytes(message);
                    listenSocket.SendTo(data, remoteEndPoint);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}

