using System;
using System.Windows;

namespace Lucky6Controller
{
    /// <summary>
    /// Interaction logic for EnterConMsg.xaml
    /// </summary>
    public partial class EnterConMsg : Window
    {
		public EnterConMsg(string defPort, string defHost)
		{
			InitializeComponent();
			Host = defHost;
			Port = defPort;
		}

		private void btnDialogOk_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = true;
		}

		private void Window_ContentRendered(object sender, EventArgs e)
		{
			txtHost.SelectAll();
			txtHost.Focus();
		}

		public string Host
		{
            set { txtHost.Text = value; }
			get { return txtHost.Text; }
		}
		public string Port
		{
			set { txtPort.Text = value; }
			get { return txtPort.Text; }
		}
	}
}
