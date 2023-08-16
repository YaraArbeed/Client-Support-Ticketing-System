using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticketing.Models.PocoModels
{
   public class SignInResponse
    {
       public string Token { get; set; }
        public string Message { get; set; }

        public int RoleId { get; set; }
    }
}
