using QueuServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QueueTerminal.Models
{
    [DataContract]
    public class ServerUpdate
    {
        [DataMember(Name = "t")]
        public List<TerminalTicket> Tickets { get; set; }

        public TerminalTicket GetCurrentTicket()
        {
            if (Tickets == null || Tickets.Count == 0)
                return null;

            foreach (var t in Tickets)
                if (t.clientId == _AppData.TerminalId)
                    return t;

            return null;
        }
    }
}
