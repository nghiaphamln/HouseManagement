﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Repositories.DbContext;

#nullable disable

namespace Repositories.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240113170635_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Models.Entities.GroupDetailEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp")
                        .HasColumnName("created_date");

                    b.Property<string>("CreatedUser")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("created_user");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<int>("GroupId")
                        .HasColumnType("integer")
                        .HasColumnName("group_id");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("timestamp")
                        .HasColumnName("last_updated");

                    b.Property<string>("UpdatedUser")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("updated_user");

                    b.HasKey("Id");

                    b.ToTable("group_detail");
                });

            modelBuilder.Entity("Models.Entities.GroupEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp")
                        .HasColumnName("created_date");

                    b.Property<string>("CreatedUser")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("created_user");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasColumnName("group_name");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("timestamp")
                        .HasColumnName("last_updated");

                    b.Property<int?>("LimitMember")
                        .HasColumnType("integer")
                        .HasColumnName("limit_member");

                    b.Property<string>("Note")
                        .HasColumnType("varchar(200)")
                        .HasColumnName("note");

                    b.Property<string>("UpdatedUser")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("updated_user");

                    b.HasKey("Id");

                    b.ToTable("group");
                });

            modelBuilder.Entity("Models.Entities.UserEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Avatar")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasColumnName("avatar");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp")
                        .HasColumnName("created_date");

                    b.Property<string>("CreatedUser")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("created_user");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("timestamp")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasColumnName("full_name");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_deleted");

                    b.Property<DateTime?>("LastUpdated")
                        .HasColumnType("timestamp")
                        .HasColumnName("last_updated");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasColumnName("password");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("phone");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("varchar(20)")
                        .HasColumnName("role");

                    b.Property<string>("UpdatedUser")
                        .HasColumnType("varchar(100)")
                        .HasColumnName("updated_user");

                    b.HasKey("Id");

                    b.ToTable("user");
                });
#pragma warning restore 612, 618
        }
    }
}
