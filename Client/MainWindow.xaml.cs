using Microsoft.Win32;
using System.IO;
using System.Net.Sockets;
using System.Windows;
using TcpLib;

namespace Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonSelect_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if(dialog.ShowDialog() == true)
            {
                t1.Text = dialog.FileName;
            }
        }

        private async void ButtonSend_Click(object sender, RoutedEventArgs e)
        {
            if (t1.Text.Length==0)
            {
                MessageBox.Show("Please select file first", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            using TcpClient server = new TcpClient("localhost", 2025);

            await server.SendStringAsync(t1.Text);
            using Stream stream = File.OpenRead(t1.Text);
            await server.SendFileAsync(stream);

            t1.Text = "";
            MessageBox.Show("File was sent successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}