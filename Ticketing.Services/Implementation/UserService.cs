using Masegat.Repository.Implementation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositories.Implementation;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Ticketing.BuisinessLayer.Interface;
using Ticketing.Models.Models;
using Ticketing.Models.PocoModels;
using Ticketing.Services.Interface;
using Tickiting.Utility;

namespace Ticketing.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService ;

        public UserService(IUserRepository userRepository,ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }
        public async Task<SignInResponse> LoginAsync(SignInParam Param)
        {
            // Retrieve the user by email
            User user = await _userRepository.GetUserByUserNameAsync(Param.UserName);

            if (user == null)
            {
                return new SignInResponse { Message="UserName isn't Found"};
            }
          

            // Verify the password
            bool isPasswordValid = VerifyPassword(Param.Password, user.Password);

            if (!isPasswordValid)
            {
               return new SignInResponse { Message = "Invalid UserName or Password" };
            }

            if (user.IsActive == false)
            {
                return new SignInResponse { Message = "This user isn't allowed to enter the system" };
            }

            // Generate and return a token (you can use a JWT library or any other token generation mechanism)
            var token = _tokenService.GenerateJsonWebToken(user);

            return new SignInResponse {Message="Login Succefully" ,Token= new JwtSecurityTokenHandler().WriteToken(token),RoleId=user.RoleId };
        }
        public async Task<RegisterResponse> RegisterUserAsync(UserRegistrationParam Param)
        {

            // Check if the user is already registered
            User user = await _userRepository.GetUserByUserNameAsync(Param.UserName);

            if (user != null)
            {
                return new RegisterResponse { Message = "User is already Registerd" };
            }

            // Create a new User object and populate its properties
            var newUser = new User
            {
        
              Name = Param.Name,
                Password = Helpers.CreateMD5(Param.Password),
                Email=Param.Email,
                DateOfBirth=Param.DateOfBirth,
                Address=Param.Address,
            IsActive=true,
            RoleId=Param.RoleId,
            UserName=Param.UserName,
            MobileNumber=Param.MobileNumber

               
            };

            // Save the new user to the repository
            await _userRepository.AddAsync(newUser);

            return new RegisterResponse { Message = "You are Registerd Correctly!" };



        }


        public async Task<RegisterResponse> ActivateUserAsync(int userId)
        {
            User user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                return new RegisterResponse { Message = "UserId isn't Found" };
            }

            user.IsActive = true;
            await _userRepository.SaveAsync();
            return new RegisterResponse { Message = "The user activation was successfully completed" };

        }
        public async Task<RegisterResponse> DeactiveUserAsync(int id)
        {
            User user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return new RegisterResponse { Message = "UserId isn't Found" };
            }

            user.IsActive = false;
            await _userRepository.SaveAsync();

            return new RegisterResponse { Message = "The user deactivation was successfully completed" };
        }
        // Helper method to verify the password
        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            // Assuming the stored password is hashed using MD5
            string hashedEnteredPassword = Helpers.CreateMD5(enteredPassword);

            // Compare the entered password with the stored hashed password
            return hashedEnteredPassword == storedPassword;
        }

        
       
    }

   
}
