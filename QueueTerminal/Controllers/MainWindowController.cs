using QueueTerminal.Interfaces;
using QueueTerminal.Models;
using QueuServer;
using QueuServer.Managers;
using System.Threading;
using System.Threading.Tasks;

namespace QueueTerminal.Controllers
{
    class MainWindowController : IServerUpdateListener
    {
        private MainWindow window;
        private ServerManager serverManager;

        public ServerUpdate data { get; set; }

        public MainWindowController(MainWindow window)
        {
            this.window = window;
            InitializeServer();
        }

        private void InitializeServer()
        {
            serverManager = new ServerManager(this);
            //new Thread(() => serverManager.Initialize()).Start();
            var initialized = serverManager.Initialize();
            if (!initialized)
                window.DisplayNotInitialized();
        }

        public async Task RequestNextTicket()
        {
            int currTicketId = -1;
            var currentTicket = data.GetCurrentTicket();
            if (currentTicket != null)
                currTicketId = currentTicket.id;

            // In case currTicketId is still -1 it means
            // that we do not have any tickets yet. 

            var ticketRequest = new TicketRequest(currTicketId);
            var json = SerializationManager<TicketRequest>.Serialize(ticketRequest);
            await serverManager.SendDataToServerAsync(json);
        }

        public void NewListReceived(ServerUpdate update)
        {
            this.data = update;
            window.NewListReceived(data);

            var currTicket = update.GetCurrentTicket();
            if (currTicket != null)
                window.UpdateCurrentTicket(currTicket.number);
        }
    }
}
