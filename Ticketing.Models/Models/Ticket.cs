using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketing.Models.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int StateId { get; set; }
        public int AssigneeId { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public string Attachments { get; set; }

        public User Assignee { get; set; }
        public Product Product { get; set; }
        public User Customer { get; set; }
        public State State { get; set; }

    }
}
