using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.Models.PocoModels;

namespace Ticketing.Services.Interface
{
    public interface IUserService
    {
        Task<SignInResponse> LoginAsync(SignInParam Param);
        Task<RegisterResponse> RegisterUserAsync(UserRegistrationParam Param);
    }
}
