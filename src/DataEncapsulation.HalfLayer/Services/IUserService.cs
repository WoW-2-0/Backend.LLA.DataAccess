using DataEncapsulation.HalfLayer.Models.Entities;

namespace DataEncapsulation.HalfLayer.Services;

public interface IUserService
{
    ValueTask<IList<User>> GetAsync();

    ValueTask<User?> GetByIdAsync(Guid id);

    ValueTask<User> CreateAsync(User user);

    ValueTask<User> UpdateAsync(User user);

    ValueTask<User> DeleteAsync(Guid id);
}