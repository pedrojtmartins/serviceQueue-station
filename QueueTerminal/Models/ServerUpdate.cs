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
        [DataMember(Name = "nt")]
        public TerminalTicket nextTicket { get; set; }

        [DataMember(Name = "t")]
        public List<TerminalTicket> tickets { get; set; }

        public TerminalTicket GetCurrentTicket()
        {
            if (nextTicket == null)
                return null;

            if (nextTicket.clientId == _AppData.TerminalId)
                return nextTicket;

            return null;
        }
    }
}
