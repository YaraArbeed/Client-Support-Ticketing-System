using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketing.Models.Models
{
    public class UserType
    {
        public int Id { get; set; }
        [Column(TypeName = "Nvarchar(100)")]
        public string Name { get; set; }
    }
}
