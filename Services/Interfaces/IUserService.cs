using AI_Project.Models.UserModels;
using AI_Project.ViewModels;

namespace AI_Project.Services.Interfaces
{
    public interface IUserService
    {
        public Task<bool> CreateAdminUserModelAsync(AdminUserViewModel user);
        public Task<(bool,string)> LoginUserAsync(AdminUserViewModel user);
        public void LogoutUser() {}
        public Task UpdateAdminUserModelAsync(AdminUserViewModel user);
        public SubjectUserViewModel CreateSubjectUser();
        Task<SubjectUserModel?> GetUserModelAsync(Guid userId);
    }
}
