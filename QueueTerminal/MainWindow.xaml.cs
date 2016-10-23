using QueueTerminal.Controllers;
using QueueTerminal.Interfaces;
using QueueTerminal.Models;
using QueuServer;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task OnClick_TicketComplete()
        {
            await controller.RequestNextTicket();
        }
    }
}
