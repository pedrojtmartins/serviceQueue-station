using QueueTerminal.Controllers;
using QueueTerminal.Models;
using System.Windows;
using System;

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

            controller = new MainWindowController(this);
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

        private void Click_Config(object sender, RoutedEventArgs e)
        {
            if (config.Visibility == Visibility.Visible)
                config.Visibility = Visibility.Hidden;
            else
                config.Visibility = Visibility.Visible;
        }

        private void Click_Restart(object sender, RoutedEventArgs e)
        {
            //controller = new MainWindowController(this);
        }

        public void NoMoreTicketsAvailable()
        {
            this.Dispatcher.Invoke(() =>
            {
                currentTicket.Text = "Sem senhas novas.";
            });
        }
    }
}
