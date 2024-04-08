using CleanArchitecture.Infrastructure.Data;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;
using CleanArchitecture.Domain.Entities;
namespace CleanArchitecture.Tests
{
    public class UserRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public UserRepositoryTests()
        {
                _options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;
        }
        [Fact]
        public async Task Addsync_ShouldAddUser_WhenUserIsValid()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                var userRepository = new UserRepository(context);
                var user = new User { Name = "Test user", Email = "Test@email.com" };

                await userRepository.Addsync(user);

                var userInDb = await context.Users.FirstOrDefaultAsync(u => u.Email == "Test@email.com");
                Assert.NotNull(userInDb);
                Assert.Equal("Test user", userInDb.Name);

            }
        }
        [Fact]
        public async Task EmailExistsAsync_ShouldReturnTrue_WhenEmailExists()
        {
            using (var context = new ApplicationDbContext(_options))
            {
                context.Users.Add(new User { Name = "Existing User", Email = "Existing@example.com" });
                context.SaveChanges();

                var userRepository = new UserRepository(context);

                var exists = await userRepository.EmailExistsAsync("Existing@example.com");
                Assert.True(exists);
            }
        }
    }
}
