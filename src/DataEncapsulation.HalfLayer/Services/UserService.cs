using DataEncapsulation.HalfLayer.Models.Entities;
using FileBaseContext.Abstractions.Models.FileSet;
using FileBaseContext.Set.Models.FileSet;

namespace DataEncapsulation.HalfLayer.Services;

public class UserService : IUserService
{
    private readonly IFileSet<User, Guid> _users;

    public UserService(IWebHostEnvironment environment)
    {
        _users = new FileSet<User, Guid>(Path.Combine(environment.ContentRootPath, "Data", "Storage"), null, null);
        _users.FetchAsync().AsTask().Wait();

        if (!_users.Any())
            _users.AddRangeAsync(new[]
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "John",
                    LastName = "Doe",
                    EmailAddress = "john.doe@gmail.com",
                    Password = "123456",
                    CreatedTime = DateTime.UtcNow,
                    ModifiedTime = null,
                    DeletedTime = null,
                    IsDeleted = false
                }
            });
    }

    public ValueTask<IList<User>> GetAsync()
    {
        return _users.ToListAsync();
    }

    public ValueTask<User?> GetByIdAsync(Guid id)
    {
        var user = _users.FirstOrDefault(x => x.Id == id);
        return new ValueTask<User?>(user);
    }

    public async ValueTask<User> CreateAsync(User user)
    {
        user.CreatedTime = DateTime.Now;

        await _users.AddAsync(user);
        await _users.SaveChangesAsync();

        return user;
    }

    public async ValueTask<User> UpdateAsync(User user)
    {
        var foundUser = await _users.FindAsync(user.Id) ?? throw new InvalidOperationException();

        foundUser.FirstName = user.FirstName;
        foundUser.LastName = user.LastName;
        foundUser.EmailAddress = user.EmailAddress;
        foundUser.Password = user.Password;

        foundUser.ModifiedTime = DateTime.Now;

        await _users.UpdateAsync(foundUser);
        await _users.SaveChangesAsync();

        return foundUser;
    }

    public async ValueTask<User> DeleteAsync(Guid id)
    {
        var foundUser = await _users.FindAsync(id) ?? throw new InvalidOperationException();

        foundUser.IsDeleted = true;
        foundUser.DeletedTime = DateTimeOffset.Now;

        await _users.UpdateAsync(foundUser);
        await _users.SaveChangesAsync();

        return foundUser;
    }
}