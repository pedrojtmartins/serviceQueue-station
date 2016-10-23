using QueueTerminal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QueuTerminal.Models.Terminal
{
    [DataContract]
    class ConnectionIdentification
    {
        [DataMember(Name = "t")]
        public bool IsTerminal { get { return true; } }

        [DataMember(Name = "i")]
        public int TerminalId { get { return _AppData.TerminalId; } }
    }
}
