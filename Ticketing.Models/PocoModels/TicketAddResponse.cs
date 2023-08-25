using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketing.Models.PocoModels
{
    public class TicketAddResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
