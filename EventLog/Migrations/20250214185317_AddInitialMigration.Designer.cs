﻿// <auto-generated />
using System;
using EventLog.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EventLog.Migrations
{
    [DbContext(typeof(EventLogDbContext))]
    [Migration("20250214185317_AddInitialMigration")]
    partial class AddInitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.18");

            modelBuilder.Entity("EventLog.Models.Entities.EntityLogEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ActionType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EntityId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EntityType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EventLogEntryId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EventLogEntryId");

                    b.ToTable("EntityLog", "eventlog");
                });

            modelBuilder.Entity("EventLog.Models.Entities.EventLogEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CompletedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("CreatedBy")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Details")
                        .HasColumnType("TEXT");

                    b.Property<int>("EventType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FailureDetails")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("EventLog", "eventlog");
                });

            modelBuilder.Entity("EventLog.Models.Entities.PropertyLogEntryModels.BoolPropertyLogEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EntityLogEntryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PropertyType")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Value")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EntityLogEntryId");

                    b.ToTable("BoolPropertyLog", "eventlog");
                });

            modelBuilder.Entity("EventLog.Models.Entities.PropertyLogEntryModels.DecimalPropertyLogEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EntityLogEntryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PropertyType")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EntityLogEntryId");

                    b.ToTable("DecimalPropertyLog", "eventlog");
                });

            modelBuilder.Entity("EventLog.Models.Entities.PropertyLogEntryModels.EventStatusDescription", b =>
                {
                    b.Property<int>("EnumId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.HasKey("EnumId");

                    b.ToTable("EventStatusDescriptions", "eventlog");
                });

            modelBuilder.Entity("EventLog.Models.Entities.PropertyLogEntryModels.EventTypeDescription", b =>
                {
                    b.Property<int>("EnumId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.HasKey("EnumId");

                    b.ToTable("EventTypeDescriptions", "eventlog");
                });

            modelBuilder.Entity("EventLog.Models.Entities.PropertyLogEntryModels.Int32PropertyLogEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EntityLogEntryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PropertyType")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Value")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EntityLogEntryId");

                    b.ToTable("Int32PropertyLog", "eventlog");
                });

            modelBuilder.Entity("EventLog.Models.Entities.PropertyLogEntryModels.StringPropertyLogEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("EntityLogEntryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PropertyType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EntityLogEntryId");

                    b.ToTable("StringPropertyLog", "eventlog");
                });

            modelBuilder.Entity("EventLog.Models.Entities.EntityLogEntry", b =>
                {
                    b.HasOne("EventLog.Models.Entities.EventLogEntry", "EventLogEntry")
                        .WithMany("EntityLogEntries")
                        .HasForeignKey("EventLogEntryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("EventLogEntry");
                });

            modelBuilder.Entity("EventLog.Models.Entities.PropertyLogEntryModels.BoolPropertyLogEntry", b =>
                {
                    b.HasOne("EventLog.Models.Entities.EntityLogEntry", "EntityLogEntry")
                        .WithMany("BoolPropertyLogEntries")
                        .HasForeignKey("EntityLogEntryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("EntityLogEntry");
                });

            modelBuilder.Entity("EventLog.Models.Entities.PropertyLogEntryModels.DecimalPropertyLogEntry", b =>
                {
                    b.HasOne("EventLog.Models.Entities.EntityLogEntry", "EntityLogEntry")
                        .WithMany("DecimalPropertyLogEntries")
                        .HasForeignKey("EntityLogEntryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("EntityLogEntry");
                });

            modelBuilder.Entity("EventLog.Models.Entities.PropertyLogEntryModels.Int32PropertyLogEntry", b =>
                {
                    b.HasOne("EventLog.Models.Entities.EntityLogEntry", "EntityLogEntry")
                        .WithMany("Int32PropertyLogEntries")
                        .HasForeignKey("EntityLogEntryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("EntityLogEntry");
                });

            modelBuilder.Entity("EventLog.Models.Entities.PropertyLogEntryModels.StringPropertyLogEntry", b =>
                {
                    b.HasOne("EventLog.Models.Entities.EntityLogEntry", "EntityLogEntry")
                        .WithMany("StringPropertyLogEntries")
                        .HasForeignKey("EntityLogEntryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("EntityLogEntry");
                });

            modelBuilder.Entity("EventLog.Models.Entities.EntityLogEntry", b =>
                {
                    b.Navigation("BoolPropertyLogEntries");

                    b.Navigation("DecimalPropertyLogEntries");

                    b.Navigation("Int32PropertyLogEntries");

                    b.Navigation("StringPropertyLogEntries");
                });

            modelBuilder.Entity("EventLog.Models.Entities.EventLogEntry", b =>
                {
                    b.Navigation("EntityLogEntries");
                });
#pragma warning restore 612, 618
        }
    }
}
