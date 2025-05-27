using AI_Project.ViewModels;

namespace AI_Project.Services.Interfaces
{
    public interface IUserService
    {
        public Task<bool> CreateUserModelAsync(AdminUserViewModel user);
        public Task<(bool,string)> LoginUserAsync(AdminUserViewModel user);
        public void LogoutUser() {}
        public Task UpdateUserModelAsync(AdminUserViewModel user);
    }
}
