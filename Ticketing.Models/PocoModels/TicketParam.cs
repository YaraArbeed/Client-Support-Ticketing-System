﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketing.Models.PocoModels
{
    public class TicketParam
    {
       public string Title { get; set; }
        public string Description { get; set; }
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public string Attachments { get; set; }
    }
}
