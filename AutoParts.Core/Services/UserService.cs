using AutoParts.Core.Contracts;
using AutoParts.Core.Models.User;
using AutoParts.Infrastructure.Data.Models;
using AutoParts.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoParts.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbRepository repo;

        public UserService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task<User> GetUserById(string id)
        {
            return await repo.GetByIdAsync<User>(id);
        }

        public async Task<UserEditViewModel> GetUserForEdit(string id)
        {
            var user = await repo.GetByIdAsync<User>(id);

            return new UserEditViewModel()
            {
                Id = user.Id,
                FullName = user.FullName
            };
        }

        public async Task<IEnumerable<UserListViewModel>> GetUsers()
        {
            return await repo.All<User>()
                .Select(u => new UserListViewModel()
                {
                    Email = u.Email,
                    Id = u.Id,
                    Name = u.FullName
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateUser(UserEditViewModel model)
        {
            //TODO: add try-catch 
            bool result = false;

            var user = await repo.GetByIdAsync<User>(model.Id);

            if (user != null)
            {
                user.FullName = model.FullName;

                await repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }
    }
}
