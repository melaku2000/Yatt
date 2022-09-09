using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yatt.Models.Entities;
using Yatt.Models.Enums;

namespace Yatt.Api.Data
{
    public class RoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            var current = DateTime.UtcNow;
            //byte[] hash, salt;

            //PasswordHasher.GeneratePasswordHasing("Melaku@12", out salt, out hash);
            builder.HasData(new UserRole
            {
                UserId = "melaku1234",
                CreatedDate = current,
                Role = RoleType.SuperAdmin,
                ModifyDate = current,
            });
        }
    }
}
