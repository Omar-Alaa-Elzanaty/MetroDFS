﻿// <auto-generated />
using MetroDFS.Presistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MetroDFS.Presistance.Migrations
{
    [DbContext(typeof(MetroDFSContext))]
    [Migration("20241016210538_InitialCreation")]
    partial class InitialCreation
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MetroDFS.Models.Models.ChildStation", b =>
                {
                    b.Property<int>("StationId")
                        .HasColumnType("int");

                    b.Property<int>("ParentStationId")
                        .HasColumnType("int");

                    b.Property<byte>("Line")
                        .HasColumnType("tinyint");

                    b.Property<string>("LineDirection")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StationId", "ParentStationId");

                    b.HasIndex("ParentStationId");

                    b.ToTable("ChildrensStations");
                });

            modelBuilder.Entity("MetroDFS.Services.Models.Station", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Stations");
                });

            modelBuilder.Entity("MetroDFS.Models.Models.ChildStation", b =>
                {
                    b.HasOne("MetroDFS.Services.Models.Station", "ParentStation")
                        .WithMany()
                        .HasForeignKey("ParentStationId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("MetroDFS.Services.Models.Station", "Station")
                        .WithMany("Childs")
                        .HasForeignKey("StationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ParentStation");

                    b.Navigation("Station");
                });

            modelBuilder.Entity("MetroDFS.Services.Models.Station", b =>
                {
                    b.Navigation("Childs");
                });
#pragma warning restore 612, 618
        }
    }
}
