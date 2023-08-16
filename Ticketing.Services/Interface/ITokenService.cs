
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Models.Models;

namespace Ticketing.BuisinessLayer.Interface
{
    public interface ITokenService
    {
        JwtSecurityToken GenerateJsonWebToken(User user);
    }
}