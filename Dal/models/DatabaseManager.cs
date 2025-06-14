﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Dal.models;

public partial class DatabaseManager : DbContext
{
    public DatabaseManager()
    {
    }

    public DatabaseManager(DbContextOptions<DatabaseManager> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"D:\\Users\\User\\Desktop\\גרסא אחרונה\\2\\DB\\DataBase.mdf\";Integrated Security=True;Connect Timeout=30");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC079108A419");

            entity.ToTable("Address");

            entity.Property(e => e.City)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Street)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07A0DF506D");

            entity.Property(e => e.ClientId)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Date).HasColumnType("datetime");

            entity.HasOne(d => d.Client).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__Clien__571DF1D5");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Appointme__Docto__5629CD9C");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Clients__3214EC0762DFBE81");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.HealthInsurance).HasMaxLength(50);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.Address).WithMany(p => p.Clients)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK__Clients__Address__5DCAEF64");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tmp_ms_x__3214EC07E3005640");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsFixedLength();
            entity.Property(e => e.Specialization).HasMaxLength(50);

            entity.HasOne(d => d.Address).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Doctors__Address__5EBF139D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
