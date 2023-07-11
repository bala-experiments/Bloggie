using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;

namespace Bloggie.Web.Data
{
    public class AuthDbContext : IdentityDbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Create roles for the application
            //superadmin,admin,user.
            //creating list of users.

            var superadminroleID = "8adcf997-50b4-46a2-b576-45dc2ddae900";
            var adminroleID = "d6da5e81-0462-4be5-8aee-e01a9d190288";
            var userroleID = "ee36e58e-8b9b-4aa7-a210-b197d7e3a1f1";

            var roles = new List<IdentityRole>() {

                new IdentityRole
                {
                    Name = "SuperAdmin",
                    NormalizedName = "SuperAdmin",
                    Id = superadminroleID,
                    ConcurrencyStamp = superadminroleID
                },
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin",
                    Id = adminroleID,
                    ConcurrencyStamp = adminroleID
                },
                new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User",
                    Id = userroleID,
                    ConcurrencyStamp = userroleID
                }

            };

            builder.Entity<IdentityRole>().HasData(roles);

            //creating roles completed.


            //Creating identity users

            var superadminuserid = "5e723099-e2ed-4433-a263-e0a84dcae4a9";

            var superAdminUser = new IdentityUser()
            {
                UserName = "bala.kodis@gmail.com",
                Email   = "bala.kodis@gmail.com",
                NormalizedEmail = "bala.kodis@gmail.com".ToUpper(),
                NormalizedUserName = "bala.kodis@gmail.com".ToUpper(),
                Id = superadminuserid
            };

            superAdminUser.PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(superAdminUser, "superadmin@123");

            builder.Entity<IdentityUser>().HasData(superAdminUser);


            //Creating identityuser roles.
            //superadmin have all the three roles hence adding it

            var SuperAdminRoles = new List<IdentityUserRole<string>> //key between two tables is a string , so that adding it with identityuserrole object
            {
                new IdentityUserRole<string>()
                {
                    RoleId = adminroleID.ToString(),
                    UserId = superadminuserid.ToString(),
                },
                new IdentityUserRole<string>()
                {
                    RoleId = superadminroleID,
                    UserId = superadminuserid.ToString(),
                },
                new IdentityUserRole<string>()
                {
                    RoleId = userroleID,
                    UserId = superadminuserid.ToString(),
                }

            };

            builder.Entity<IdentityUserRole<string>>().HasData(SuperAdminRoles);

            


        }
    }
}
