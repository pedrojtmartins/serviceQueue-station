using QueueTerminal.Controllers;
using QueueTerminal.Models;
using System.Windows;
using System;
using QueueTerminal.Config;

namespace QueueTerminal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowController controller;

        public MainWindow()
        {
            InitializeComponent();
            PopulateConfig();
        }

        private void PopulateConfig()
        {
            var data = ConfigHelper.GetConfig();
            if (data == null || data.Length != 2)
                return;

            numP.Text = data[0];
            ip.Text = data[1];
        }

        public void NewListReceived(ServerUpdate update)
        {
            this.Dispatcher.Invoke(() =>
            {    if (update == null)
                {
                    errorTB.Text = "A ligação ao servidor foi perdida.\nAssim que este esteja operacional reinicie esta aplicação.";
                    waiting.Visibility = Visibility.Visible;
                }
                else
                {
                    waiting.Visibility = Visibility.Hidden;
                }
            });
        }

        private async void Click_Next(object sender, RoutedEventArgs e)
        {
            waiting.Visibility = Visibility.Visible;
            await controller.RequestNextTicket();
        }

        public void UpdateCurrentTicket(string ticket)
        {
            this.Dispatcher.Invoke(() =>
            {
                currentTicket.Text = ticket;
            });
        }

        public void DisplayNotInitialized()
        {
            config.Visibility = Visibility.Visible;
        }

        public void Click_Config()
        {

        }

        private void Click_Restart(object sender, RoutedEventArgs e)
        {
            config.Visibility = Visibility.Hidden;

            string sPostNum = numP.Text;
            string sIp = ip.Text;

            int iPost = 0;
            int.TryParse(sPostNum, out iPost);
            _AppData.TerminalId = iPost;

            controller = new MainWindowController(this, sIp);

            ConfigHelper.SaveConfig(sPostNum, sIp);
        }

        public void NoMoreTicketsAvailable()
        {

            this.Dispatcher.Invoke(() =>
            {
                waiting.Visibility = Visibility.Hidden;
                currentTicket.Text = "Sem senhas novas.";
            });
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (controller != null)
                controller.CloseConnection();
        }
    }
}
