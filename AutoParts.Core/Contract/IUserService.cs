namespace AutoParts.Core.Contracts
{
    using AutoParts.Core.Models.User;
    using AutoParts.Infrastructure.Data.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IUserService
    {
        Task<IEnumerable<UserListViewModel>> GetUsers();

        Task<UserEditViewModel> GetUserForEdit(string id);

        Task<bool> UpdateUser(UserEditViewModel model);

        Task<User> GetUserById(string id);
    }
}
