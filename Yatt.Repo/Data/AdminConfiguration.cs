using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yatt.Repo.Handlers;
using Yatt.Models.Entities;

namespace Yatt.Repo.Data
{
    public class AdminConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            var current = DateTime.UtcNow;
            byte[] hash, salt;

            PasswordHasher.GeneratePasswordHasing("Melaku@12", out salt, out hash);
            builder.HasData(new User
            {
                Id = "melaku1234",
                Email = "melakumen@gmail.com",
                CreatedDate = current,
                ModifyDate = current,
                EmailConfirmed = true,
                DeletedDate = null,
                PasswordHash = hash,
                PasswordSalt = salt
            });
        }
    }
}
