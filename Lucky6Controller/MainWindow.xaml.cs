using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace Lucky6Controller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UdpClient _udpClient;
        private IPEndPoint _ipAddress;
        private string _host;
        private int _port;
        private readonly ConcurrentQueue<string> _messages = new ConcurrentQueue<string>();
        private bool _isConnected;

        public MainWindow()
        {
            InitializeComponent();
            Loaded += Window_Loaded;
            Connect();
                        
       
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GameModeCbx.SelectedIndex = 0;
        }

        public void Connect()
        {
            EnterConMsg inputDialog = new EnterConMsg("3333", "127.0.0.1");
            if (inputDialog.ShowDialog() == true)
            {
                _host = inputDialog.Host;
                _port = Int32.Parse(inputDialog.Port);
            }
            else
            {
                _host = "127.0.0.1";
                _port = 3333;
            }
            HostLbl.Content = "Host: " + _host;
            PortLbl.Content = "Port: " + _port;
            _ipAddress = new IPEndPoint(IPAddress.Parse(_host), _port);
            _udpClient = new UdpClient();
            _udpClient.Connect(_ipAddress);
            _isConnected = true;
        }

        private void SendNextItem()
        {
            if (_messages.TryDequeue(out var current))
            {
                try
                {
                    if (!_isConnected)
                    {
                        _udpClient.Connect(_ipAddress);
                        _isConnected = true;
                    }

                    var data = Encoding.ASCII.GetBytes(current);
                    _udpClient.Send(data, data.Length);
                }
                catch (Exception e)
                {
                    _isConnected = false;
                    CloseSocket();
                }
            }
        }

       

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CloseSocket()
        {
            try
            {
                _udpClient?.Close();
            }
            catch { }
        }

        private void ReconnectBtn_Click(object sender, RoutedEventArgs e)
        {
            Connect();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedIndex == 0)
            {
                Send("Lotto");
            }
            else
            {
                Send("SpinTheWheel");
                STWModeCbx.IsEnabled = true;
                STWModeCbx.SelectedIndex = 1;
            }
        }

        private void Send(string command)
        {
            _messages.Enqueue(command);
            SendNextItem();
        }

        private void STWCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch ((sender as ComboBox).SelectedIndex)
            {
                case 0:
                    Send("Mode1");
                    break;
                case 1:
                    Send("Mode2");
                    break;
                case 2:
                    Send("Mode3");
                    break;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if ((sender as CheckBox).IsChecked ?? false)
                Send("ShowAvatar");
            else
                Send("HideAvatar");
            
        }

        private void TriggerAvatarCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Send((sender as ComboBox).Text);
        }

        private void AvatarScaleSrlBar_Scroll(object sender, ScrollEventArgs e)
        {
            Send("AvatarScale:" + (sender as ScrollBar).Value);
            AvaScaleLbl.Content = (sender as ScrollBar).Value;
        }

        private void SetAvaPosBtn_Click(object sender, RoutedEventArgs args)
        {
            Send($"AvatarPosition:{AvaPosXTxt.Text},{AvaPosYTxt.Text}");
        }

        private void ZoomCbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Send("ZoomTo" + ((sender as ComboBox).SelectedIndex == 0 ? "Screen" : "Model"));
        }
    }
}
