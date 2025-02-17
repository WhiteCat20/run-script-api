﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using run_script.Data;

#nullable disable

namespace run_script.Migrations
{
    [DbContext(typeof(ScriptDbContext))]
    partial class ScriptDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("run_script.Models.Script", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("LifeStatus")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ScriptContent")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TimesAccessed")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Scripts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            LifeStatus = false,
                            Name = "Hello World",
                            ScriptContent = "echo \"Hello\" ",
                            TimesAccessed = 0
                        },
                        new
                        {
                            Id = 2,
                            LifeStatus = false,
                            Name = "faiz",
                            ScriptContent = "docker start nginxfaiz",
                            TimesAccessed = 0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
