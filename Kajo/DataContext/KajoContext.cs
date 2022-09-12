using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Kajo.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Kajo.Models.Entities.Base;
using Kajo.Infrastucture.Identity;

namespace Kajo.DataContext
{
    public class KajoContext : IdentityDbContext
    {

        //HOWTO: ADD DBSets for models here
        //Run: PM> Update-Database

        //HOWTO: [Add entities to DB] Add DBSet for each new table, then run Add-Migration {{Name}}, Update-Database


        /* Define a DbSet for each entity of the application */

        public DbSet<KajoUser> KajoUser { get; set; }


        public DbSet<Topic> Topics { get; set; }
        public DbSet<Post> Posts { get; set; }

        public KajoContext(DbContextOptions<KajoContext> options)
            : base(options)
        {
            //Ensure database is created
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Disable cascade delete for 
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
            SeedData(modelBuilder);
            //Seed data Here
        }

        protected void SeedData(ModelBuilder modelBuilder)
        {
            var superAdminRole = new IdentityRole("SuperAdmin");
            superAdminRole.Id = Guid.NewGuid().ToString();
            superAdminRole.NormalizedName = "SUPERADMIN";

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>().HasData(superAdminRole);

            var claims = new List<IdentityRoleClaim<string>>();

            var avaliablePermissions = Permissions.GetAllPermissions();
            var i = 0;
            foreach (var permission in avaliablePermissions)
            {
                claims.Add(new IdentityRoleClaim<string>()
                {
                    RoleId = superAdminRole.Id,
                    Id = ++i,
                    ClaimType = CustomClaimTypes.Permission,
                    ClaimValue = permission,
                });
            }


            var superAdminUser = new KajoUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "SuperAdmin",
                NormalizedUserName = "SUPERADMIN",
                Email = "SuperAdmin@example.com",
                NormalizedEmail = "SUPERADMIN@EXAMPLE.COM",
                FirstName = "Super",
                LastName = "Admin",
                Team = "SuperAdminTeam",
                EmailConfirmed = true,
                About = "Application Super Admin",
                PhoneNumber = "0000000000",
                PhoneNumberConfirmed = true,
            };

            superAdminUser.PasswordHash = new PasswordHasher<KajoUser>
                (new OptionsWrapper<PasswordHasherOptions>
                (new PasswordHasherOptions()))
                .HashPassword(superAdminUser, "SuperAdmin");

            modelBuilder.Entity<KajoUser>().HasData(superAdminUser);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>()
                {
                    RoleId = superAdminRole.Id,
                    UserId = superAdminUser.Id,
                }
            );
            modelBuilder.Entity<IdentityRoleClaim<string>>().HasData(claims);
        }

        public override int SaveChanges()
        {
            HandleAuditable();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            HandleAuditable();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void HandleAuditable()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is Auditable && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((Auditable)entityEntry.Entity).UpdateDate = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                {
                    ((Auditable)entityEntry.Entity).CreationDate = DateTime.UtcNow;
                }
            }
        }
    }
}
