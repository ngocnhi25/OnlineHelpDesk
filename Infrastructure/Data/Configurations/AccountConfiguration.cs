using Domain.Entities.Accounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    internal class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(acc => acc.AccountId);
            builder.Property(acc => acc.FullName).HasMaxLength(30);
            builder.Property(acc => acc.Email).HasMaxLength(200);
            builder.Property(acc => acc.AvatarPhoto).HasMaxLength(255);
            builder.Property(acc => acc.Address).HasMaxLength(200);
            builder.Property(acc => acc.PhoneNumber).HasMaxLength(11);
            builder.Property(acc => acc.Gender).HasMaxLength(20);
            builder.Property(acc => acc.Birthday).HasMaxLength(15);
            builder.Property(acc => acc.VerifyCode).HasMaxLength(7);
            builder.Property(acc => acc.RefreshToken).HasMaxLength(255);
            builder.Property(acc => acc.StatusAccount).HasMaxLength(20);

            builder.HasOne(acc => acc.Role)
                .WithMany(role => role.Accounts)
                .HasForeignKey(acc => acc.RoleId);

            builder.HasData(new Account[]
            {
                new Account { AccountId = "ST729729", RoleId = 1, Password = "$2a$12$GPbRVLdOyRw7H1yw/.fv/uStTWDcvprTAergcVbhc7zQ3/zFAqOtW", FullName = "Duy Hiển", 
                    Email = "student@gmail.com", Address = "Bình Chánh", PhoneNumber = "0909009001", Gender = "Male", 
                    Birthday = "1975/04/30", StatusAccount = "Active", Enable = true, CreatedAt = DateTime.Now },
                new Account { AccountId = "TC729729", RoleId = 2, Password = "$2a$12$GPbRVLdOyRw7H1yw/.fv/uStTWDcvprTAergcVbhc7zQ3/zFAqOtW", FullName = "Duy Hiển", 
                    Email = "teacher@gmail.com", Address = "Bình Dương", PhoneNumber = "0909009002", Gender = "Female", 
                    Birthday = "1945/09/02", StatusAccount = "Verifying", Enable = true, CreatedAt = DateTime.Now },
                new Account { AccountId = "AS729729", RoleId = 4, Password = "$2a$12$GPbRVLdOyRw7H1yw/.fv/uStTWDcvprTAergcVbhc7zQ3/zFAqOtW", FullName = "Johnny Đãng", 
                    Email = "assignees@gmail.com", Address = "Bình Định", PhoneNumber = "0909009003", Gender = "Orther", 
                    Birthday = "1954/06/07", StatusAccount = "Active", Enable = true, CreatedAt = DateTime.Now },
                new Account { AccountId = "FH729729", RoleId = 3, Password = "$2a$12$GPbRVLdOyRw7H1yw/.fv/uStTWDcvprTAergcVbhc7zQ3/zFAqOtW", FullName = "Ngọc Nhi", 
                    Email = "facility@gmail.com", Address = "Alaska", PhoneNumber = "0909009004", Gender = "Orther", 
                    Birthday = "1975/04/30", StatusAccount = "Active", Enable = true, CreatedAt = DateTime.Now },
                new Account { AccountId = "AD729729", RoleId = 5, Password = "$2a$12$GPbRVLdOyRw7H1yw/.fv/uStTWDcvprTAergcVbhc7zQ3/zFAqOtW", FullName = "Phi Đzai", 
                    Email = "nguyentruongphi15032003@gmail.com", Address = "Alaska", PhoneNumber = "0937888707", Gender = "Orther", 
                    Birthday = "1975/04/30", StatusAccount = "Active", Enable = true, CreatedAt = DateTime.Now },
            });
        }
    }
}
