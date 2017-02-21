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
        }

        private async void Click_Next(object sender, RoutedEventArgs e)
        {
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

        }

        public void Click_Config()
        {

        }

        private void Click_Restart(object sender, RoutedEventArgs e)
        {
            string sPostNum = numP.Text;
            string sIp = ip.Text;

            controller = new MainWindowController(this, sIp);

            ConfigHelper.SaveConfig(sPostNum, sIp);
            config.Visibility = Visibility.Hidden;
        }

        public void NoMoreTicketsAvailable()
        {
            this.Dispatcher.Invoke(() =>
            {
                currentTicket.Text = "Sem senhas novas.";
            });
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            controller.CloseConnection();
        }
    }
}
