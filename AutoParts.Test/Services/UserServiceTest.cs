namespace AutoParts.Test.Services
{
    using AutoParts.Core.Contracts;
    using AutoParts.Core.Models.User;
    using AutoParts.Infrastructure.Data;
    using AutoParts.Infrastructure.Data.Models;
    using AutoParts.Infrastructure.Data.Repositories;
    using Microsoft.Extensions.DependencyInjection;
    using NUnit.Framework;
    using System.Threading.Tasks;
    using Xunit;

    public class UserServiceTest
    {
        private ServiceProvider serviceProvider;

        [Fact]
        public async Task UpdateUserMustNotUpdateWithIncorrectInformation()
        {
            var service = serviceProvider.GetService<IUserService>();

            var editedUser = new UserEditViewModel()
            {
                Id = "1",
                FullName = "testUser"
            };

            var result = await service.UpdateUser(editedUser);

            NUnit.Framework.Assert.AreEqual(false, result);
        }
    }
}
