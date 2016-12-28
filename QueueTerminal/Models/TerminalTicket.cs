using QueuServer.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QueuServer.Models
{
    [DataContract]
    public class TerminalTicket
    {
        [DataMember(Name = "i")]
        public int id { get; set; }

        [DataMember(Name = "n")]
        public string number { get; set; }

        [DataMember(Name = "t")]
        public int type { get; set; }

        [DataMember(Name = "c", IsRequired = false)]
        public Nullable<int> clientId { get; set; }
    }
}
