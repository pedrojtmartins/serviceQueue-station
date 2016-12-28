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

        public void DisplayNotInitialized()
        {
            
        }
    }
}
