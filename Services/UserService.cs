using AI_Project.DBContext;
using AI_Project.Models.UserModels;
using AI_Project.Services.Interfaces;
using AI_Project.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AI_Project.Services
{
    public class UserService : IUserService
    {
        private readonly CustomAuthenticationService _authService;
        private readonly AI_ProjectDbContext _dbContext;
        private readonly string _temporarySalt = "DinoNuggies";

        public string userIdentifier = string.Empty;
        public UserService(AI_ProjectDbContext dbContext, CustomAuthenticationService authService)
        {
            _dbContext = dbContext;
            _authService = authService;
        }

        public async Task<bool> CreateUserModelAsync(AdminUserViewModel user)
        {
            try
            {
                AdminUserModel model = new AdminUserModel()
                {
                    UserName = user.UserName,
                    Password = PasswordHash(user.Password)
                };

                var existingUser = await _dbContext.AdminUsers
                .FirstOrDefaultAsync(l => l.UserName == user.UserName);
                if (existingUser != null)
                {
                    return false;
                }

                await _dbContext.AddAsync(model);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch 
            {
                return false;
            }

        }

        public async Task<(bool,string)> LoginUserAsync(AdminUserViewModel user)
        {
            var loginModel = await _dbContext.AdminUsers
                .Where(l => l.UserName == user.UserName)
                .FirstOrDefaultAsync();

            if(loginModel == null)
            {
                return (false, "User not found");
            }

            if (loginModel.Password != PasswordHash(user.Password)) 
            {
                return (false, "Incorrect password");
            }

            var currentUser = _authService.CurrentUser;

            var identity = new ClaimsIdentity(
                [
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Role, "admin")
            ],
                "Custom Authentication");

            var newUser = new ClaimsPrincipal(identity);

            _authService.CurrentUser = newUser;
            
            return (true, "Login successful");
        }

        public void LogoutUser()
        {
            _authService.CurrentUser = new ClaimsPrincipal(new ClaimsIdentity());
        }

        public Task UpdateUserModelAsync(AdminUserViewModel user)
        {
            throw new NotImplementedException();
        }

        private string PasswordHash(string password)
        {
            string passwordWithSalt = password + _temporarySalt;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(passwordWithSalt);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                StringBuilder hashBuilder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashBuilder.Append(b.ToString("x2"));
                }

                return hashBuilder.ToString();
            }
        }
    }
}
