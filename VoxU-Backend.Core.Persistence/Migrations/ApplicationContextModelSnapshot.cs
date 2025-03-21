﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using VoxU_Backend.Core.Persistence.Contexts;

#nullable disable

namespace VoxU_Backend.Core.Persistence.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.31")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);


            modelBuilder.Entity("VoxU_Backend.Core.Domain.Entities.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

            modelBuilder.Entity("VoxU_Backend.Core.Domain.Entities.Category", b =>
                    b.Property<string>("Nombre")
                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UploadDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");
                    b.ToTable("Category", (string)null);
                    b.ToTable("Library", (string)null);
                });

            modelBuilder.Entity("VoxU_Backend.Core.Domain.Entities.Comments", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CommentUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("CommentUserPicture")
                        .HasColumnType("varbinary(max)");

                    b.Property<int?>("IdPublication")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdPublication");

                    b.ToTable("Comments", (string)null);
                });

            modelBuilder.Entity("VoxU_Backend.Core.Domain.Entities.Publications", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("Created_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ImageUrl")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("isBlocked")
                        .HasColumnType("bit");

                    b.Property<string>("userName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("userPicture")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.ToTable("Publications", (string)null);
                });

            modelBuilder.Entity("VoxU_Backend.Core.Domain.Entities.Replies", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CommentId")
                        .HasColumnType("int");

                    b.Property<string>("Reply")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReplyUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ReplyUserPicture")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CommentId");

                    b.ToTable("Replies", (string)null);
                });

            modelBuilder.Entity("VoxU_Backend.Core.Domain.Entities.Report", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("Created_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PublicationId")
                        .HasColumnType("int");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PublicationId");

                    b.ToTable("Reports", (string)null);
                });

            modelBuilder.Entity("VoxU_Backend.Core.Domain.Entities.SellPublications", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Created_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ImageUrl")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Place")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Price")
                        .HasColumnType("float");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("userPicture")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("SellPublications", (string)null);
                });

            modelBuilder.Entity("VoxU_Backend.Core.Domain.Entities.Comments", b =>
                {
                    b.HasOne("VoxU_Backend.Core.Domain.Entities.Publications", "Publications")
                        .WithMany("Comments")
                        .HasForeignKey("IdPublication");

                    b.Navigation("Publications");
                });

            modelBuilder.Entity("VoxU_Backend.Core.Domain.Entities.Replies", b =>
                {
                    b.HasOne("VoxU_Backend.Core.Domain.Entities.Comments", "Comments")
                        .WithMany("replies")
                        .HasForeignKey("CommentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Comments");
                });

            modelBuilder.Entity("VoxU_Backend.Core.Domain.Entities.Report", b =>
                {
                    b.HasOne("VoxU_Backend.Core.Domain.Entities.Publications", "Publications")
                        .WithMany("Reports")
                        .HasForeignKey("PublicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Publications");
                });

            modelBuilder.Entity("VoxU_Backend.Core.Domain.Entities.SellPublications", b =>
                {
                    b.HasOne("VoxU_Backend.Core.Domain.Entities.Category", "Category")
                        .WithMany("sellPublications")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("VoxU_Backend.Core.Domain.Entities.Category", b =>
                {
                    b.Navigation("sellPublications");
                });

            modelBuilder.Entity("VoxU_Backend.Core.Domain.Entities.Comments", b =>
                {
                    b.Navigation("replies");
                });

            modelBuilder.Entity("VoxU_Backend.Core.Domain.Entities.Publications", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Reports");
                });
#pragma warning restore 612, 618
        }
    }
}
