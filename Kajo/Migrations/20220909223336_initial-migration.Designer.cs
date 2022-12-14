// <auto-generated />
using System;
using Kajo.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Kajo.Migrations
{
    [DbContext(typeof(KajoContext))]
    [Migration("20220909223336_initial-migration")]
    partial class initialmigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Kajo.Models.Entities.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<DateTime>("CreationDate");

                    b.Property<int>("Status");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<Guid>("TopicId");

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UserID")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("TopicId");

                    b.HasIndex("UserID");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Kajo.Models.Entities.Topic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<DateTime>("UpdateDate");

                    b.Property<string>("UserID")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserID");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "a4ea560a-607b-4bd9-8b8b-03ed0a56ce3d",
                            ConcurrencyStamp = "9681ef68-b2a5-4abf-ac17-9f2721166125",
                            Name = "SuperAdmin",
                            NormalizedName = "SUPERADMIN"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Profile.Edit",
                            RoleId = "a4ea560a-607b-4bd9-8b8b-03ed0a56ce3d"
                        },
                        new
                        {
                            Id = 2,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.RolesService.UpdateRolePermissions",
                            RoleId = "a4ea560a-607b-4bd9-8b8b-03ed0a56ce3d"
                        },
                        new
                        {
                            Id = 3,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.RolesService.UpdateRoleUsers",
                            RoleId = "a4ea560a-607b-4bd9-8b8b-03ed0a56ce3d"
                        },
                        new
                        {
                            Id = 4,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.RolesService.UpdateUserRoles",
                            RoleId = "a4ea560a-607b-4bd9-8b8b-03ed0a56ce3d"
                        },
                        new
                        {
                            Id = 5,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Roles.View",
                            RoleId = "a4ea560a-607b-4bd9-8b8b-03ed0a56ce3d"
                        },
                        new
                        {
                            Id = 6,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Roles.Create",
                            RoleId = "a4ea560a-607b-4bd9-8b8b-03ed0a56ce3d"
                        },
                        new
                        {
                            Id = 7,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Roles.Edit",
                            RoleId = "a4ea560a-607b-4bd9-8b8b-03ed0a56ce3d"
                        },
                        new
                        {
                            Id = 8,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Roles.Delete",
                            RoleId = "a4ea560a-607b-4bd9-8b8b-03ed0a56ce3d"
                        },
                        new
                        {
                            Id = 9,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.UsersService.EditUserData",
                            RoleId = "a4ea560a-607b-4bd9-8b8b-03ed0a56ce3d"
                        },
                        new
                        {
                            Id = 10,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.UsersService.UpdateUserPassword",
                            RoleId = "a4ea560a-607b-4bd9-8b8b-03ed0a56ce3d"
                        },
                        new
                        {
                            Id = 11,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Users.View",
                            RoleId = "a4ea560a-607b-4bd9-8b8b-03ed0a56ce3d"
                        },
                        new
                        {
                            Id = 12,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Users.Create",
                            RoleId = "a4ea560a-607b-4bd9-8b8b-03ed0a56ce3d"
                        },
                        new
                        {
                            Id = 13,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Users.Delete",
                            RoleId = "a4ea560a-607b-4bd9-8b8b-03ed0a56ce3d"
                        },
                        new
                        {
                            Id = 14,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.WorkCenters.View",
                            RoleId = "a4ea560a-607b-4bd9-8b8b-03ed0a56ce3d"
                        },
                        new
                        {
                            Id = 15,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.WorkCenters.Create",
                            RoleId = "a4ea560a-607b-4bd9-8b8b-03ed0a56ce3d"
                        },
                        new
                        {
                            Id = 16,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.WorkCenters.Edit",
                            RoleId = "a4ea560a-607b-4bd9-8b8b-03ed0a56ce3d"
                        },
                        new
                        {
                            Id = 17,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.WorkCenters.Delete",
                            RoleId = "a4ea560a-607b-4bd9-8b8b-03ed0a56ce3d"
                        },
                        new
                        {
                            Id = 18,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Departments.View",
                            RoleId = "a4ea560a-607b-4bd9-8b8b-03ed0a56ce3d"
                        },
                        new
                        {
                            Id = 19,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Departments.Create",
                            RoleId = "a4ea560a-607b-4bd9-8b8b-03ed0a56ce3d"
                        },
                        new
                        {
                            Id = 20,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Departments.Edit",
                            RoleId = "a4ea560a-607b-4bd9-8b8b-03ed0a56ce3d"
                        },
                        new
                        {
                            Id = 21,
                            ClaimType = "permission",
                            ClaimValue = "Permissions.Departments.Delete",
                            RoleId = "a4ea560a-607b-4bd9-8b8b-03ed0a56ce3d"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");

                    b.HasData(
                        new
                        {
                            UserId = "0ccbdccf-21cd-41f9-aadc-161bf59f60c7",
                            RoleId = "a4ea560a-607b-4bd9-8b8b-03ed0a56ce3d"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Kajo.Models.Entities.KajoUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("About")
                        .HasMaxLength(2048);

                    b.Property<string>("FirstName")
                        .HasMaxLength(128);

                    b.Property<string>("LastName")
                        .HasMaxLength(128);

                    b.Property<string>("Team")
                        .HasMaxLength(128);

                    b.HasDiscriminator().HasValue("KajoUser");

                    b.HasData(
                        new
                        {
                            Id = "0ccbdccf-21cd-41f9-aadc-161bf59f60c7",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "1928aa2c-dc54-45c4-b5d7-782b6c71cc48",
                            Email = "SuperAdmin@example.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "SUPERADMIN@EXAMPLE.COM",
                            NormalizedUserName = "SUPERADMIN",
                            PasswordHash = "AQAAAAEAACcQAAAAEF3oR9XifmXrC3X/7hWPKCW6caWSLbJSVwoJYS++E6qgBmBv4TLojXzR3nV4VUWNgQ==",
                            PhoneNumber = "0000000000",
                            PhoneNumberConfirmed = true,
                            TwoFactorEnabled = false,
                            UserName = "SuperAdmin",
                            About = "Application Super Admin",
                            FirstName = "Super",
                            LastName = "Admin",
                            Team = "SuperAdminTeam"
                        });
                });

            modelBuilder.Entity("Kajo.Models.Entities.Post", b =>
                {
                    b.HasOne("Kajo.Models.Entities.Topic", "Topic")
                        .WithMany("Posts")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Kajo.Models.Entities.KajoUser", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Kajo.Models.Entities.Topic", b =>
                {
                    b.HasOne("Kajo.Models.Entities.KajoUser", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
