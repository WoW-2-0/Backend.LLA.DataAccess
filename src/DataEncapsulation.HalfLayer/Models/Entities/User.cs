using FileBaseContext.Abstractions.Models.Entity;

namespace DataEncapsulation.HalfLayer.Models.Entities;

public class User : IFileSetEntity<Guid>
{
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string EmailAddress { get; set; }

    public string Password { get; set; }

    public DateTimeOffset CreatedTime { get; set; }

    public DateTimeOffset? ModifiedTime { get; set; }

    public DateTimeOffset? DeletedTime { get; set; }

    public bool IsDeleted { get; set; }
}