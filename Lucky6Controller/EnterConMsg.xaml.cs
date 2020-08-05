using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
			get { return txtHost.Text; }
		}
		public string Port
		{
			get { return txtPort.Text; }
		}
	}
}
