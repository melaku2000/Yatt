// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Yatt.Repo.Data;

#nullable disable

namespace Yatt.Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220828184450_AddingDomain")]
    partial class AddingDomain
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Yatt.Models.Entities.Candidate", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DoBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("FatherName")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("MobilePhone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("ShowDoBirth")
                        .HasColumnType("bit");

                    b.Property<bool>("ShowPhone")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Candidates");
                });

            modelBuilder.Entity("Yatt.Models.Entities.Company", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CompanyTin")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Yatt.Models.Entities.CompanyDetail", b =>
                {
                    b.Property<string>("CompanyId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CompanyName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CompanyPhone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("ContactName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ContactPhone")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("DomainId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("WebUrl")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CompanyId");

                    b.HasIndex("CountryId");

                    b.HasIndex("DomainId");

                    b.ToTable("CompanyDetails");
                });

            modelBuilder.Entity("Yatt.Models.Entities.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DialCode")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("Yatt.Models.Entities.Domain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Domains");
                });

            modelBuilder.Entity("Yatt.Models.Entities.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("EnglishName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Yatt.Models.Entities.Profession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DomainId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("DomainId");

                    b.ToTable("Profession");
                });

            modelBuilder.Entity("Yatt.Models.Entities.RefreshToken", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IpAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TokenExpireTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserAgent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Yatt.Models.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("LockCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<bool>("PhoneConfirmed")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = "melaku1234",
                            CreatedDate = new DateTime(2022, 8, 28, 18, 44, 49, 937, DateTimeKind.Utc).AddTicks(3833),
                            Email = "melakumen@gmail.com",
                            EmailConfirmed = true,
                            LockCount = 0,
                            ModifyDate = new DateTime(2022, 8, 28, 18, 44, 49, 937, DateTimeKind.Utc).AddTicks(3833),
                            PasswordHash = new byte[] { 8, 244, 188, 157, 142, 198, 155, 183, 28, 236, 43, 219, 227, 73, 10, 93, 213, 137, 151, 163, 58, 67, 29, 119 },
                            PasswordSalt = new byte[] { 32, 79, 198, 109, 15, 16, 176, 156, 140, 115, 23, 154, 237, 231, 33, 86, 172, 166, 52, 152, 172, 243, 191, 165 },
                            PhoneConfirmed = false
                        });
                });

            modelBuilder.Entity("Yatt.Models.Entities.UserLanguage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("LangId")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("LangId");

                    b.HasIndex("UserId");

                    b.ToTable("UserLanguages");
                });

            modelBuilder.Entity("Yatt.Models.Entities.UserRole", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            UserId = "melaku1234",
                            CreatedDate = new DateTime(2022, 8, 28, 18, 44, 49, 962, DateTimeKind.Utc).AddTicks(971),
                            ModifyDate = new DateTime(2022, 8, 28, 18, 44, 49, 962, DateTimeKind.Utc).AddTicks(971),
                            Role = 102
                        });
                });

            modelBuilder.Entity("Yatt.Models.Entities.UserToken", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("IpAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TokenExpireTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("TokenType")
                        .HasColumnType("int");

                    b.Property<string>("UserAgent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("Yatt.Models.Entities.Candidate", b =>
                {
                    b.HasOne("Yatt.Models.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Yatt.Models.Entities.User", "User")
                        .WithOne("Candidate")
                        .HasForeignKey("Yatt.Models.Entities.Candidate", "Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Yatt.Models.Entities.Company", b =>
                {
                    b.HasOne("Yatt.Models.Entities.User", "User")
                        .WithOne("Company")
                        .HasForeignKey("Yatt.Models.Entities.Company", "Id")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Yatt.Models.Entities.CompanyDetail", b =>
                {
                    b.HasOne("Yatt.Models.Entities.Company", "Company")
                        .WithOne("CompanyDetail")
                        .HasForeignKey("Yatt.Models.Entities.CompanyDetail", "CompanyId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Yatt.Models.Entities.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Yatt.Models.Entities.Domain", "Domain")
                        .WithMany("Companies")
                        .HasForeignKey("DomainId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("Country");

                    b.Navigation("Domain");
                });

            modelBuilder.Entity("Yatt.Models.Entities.Profession", b =>
                {
                    b.HasOne("Yatt.Models.Entities.Domain", "Domain")
                        .WithMany("Professions")
                        .HasForeignKey("DomainId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Domain");
                });

            modelBuilder.Entity("Yatt.Models.Entities.RefreshToken", b =>
                {
                    b.HasOne("Yatt.Models.Entities.User", null)
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Yatt.Models.Entities.UserLanguage", b =>
                {
                    b.HasOne("Yatt.Models.Entities.Language", "Language")
                        .WithMany("Languages")
                        .HasForeignKey("LangId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Yatt.Models.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Language");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Yatt.Models.Entities.UserRole", b =>
                {
                    b.HasOne("Yatt.Models.Entities.User", "User")
                        .WithOne("Role")
                        .HasForeignKey("Yatt.Models.Entities.UserRole", "UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Yatt.Models.Entities.UserToken", b =>
                {
                    b.HasOne("Yatt.Models.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Yatt.Models.Entities.Company", b =>
                {
                    b.Navigation("CompanyDetail");
                });

            modelBuilder.Entity("Yatt.Models.Entities.Domain", b =>
                {
                    b.Navigation("Companies");

                    b.Navigation("Professions");
                });

            modelBuilder.Entity("Yatt.Models.Entities.Language", b =>
                {
                    b.Navigation("Languages");
                });

            modelBuilder.Entity("Yatt.Models.Entities.User", b =>
                {
                    b.Navigation("Candidate");

                    b.Navigation("Company");

                    b.Navigation("RefreshTokens");

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}
