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
            currentTicket.Text = ticket;
        }

        public void DisplayInitialized()
        {
            Dispatcher.Invoke(() => { connecting.Visibility = Visibility.Hidden; });
        }

        public void DisplayNotInitialized()
        {
            Dispatcher.Invoke(() =>
            {
                MessageBoxResult result = MessageBox.Show("Não foi possível establecer ligação ao servidor.", "", MessageBoxButton.OK, MessageBoxImage.Question);
                if (result == MessageBoxResult.OK)
                    Application.Current.Shutdown();
            });
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
    }
}
