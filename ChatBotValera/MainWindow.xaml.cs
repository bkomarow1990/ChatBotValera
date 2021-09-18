using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatBotValera
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewModel viewModel = new ViewModel();
        #region SocketInit
        // адрес и порт сервера, к которому будем подключаться
        static int port = 1337; // порт сервера
        static string address = "127.0.0.1"; // адрес сервера
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
        EndPoint remoteIpPoint = new IPEndPoint(IPAddress.Any, 0);
        
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
            this.messagesListBox.ItemsSource = viewModel.Messages;
        }
        public void InitSocket()
        {

        }
        private void sendBtn_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //viewModel.chatContext.Answers.Find(new Answer());
            string message = messageTxtBox.Text;
            byte[] data = Encoding.Unicode.GetBytes(message);
            
            socket.SendTo(data, ipPoint);

            // при використанні UDP протоколу, Connect() лише встановлює дані для відправки
            //socket.Connect(ipPoint);
            //socket.Send(data);

            // получаем ответ
            // получаем сообщение
            int bytes = 0;
            string response = "";
            data = new byte[1024];
            do
            {
                bytes = socket.ReceiveFrom(data, ref remoteIpPoint);
                response += Encoding.Unicode.GetString(data, 0, bytes);
            } while (socket.Available > 0);

            Console.WriteLine("server response: " + response);

            // закрываем сокет
            
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
    }
}
