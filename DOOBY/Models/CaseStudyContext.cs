using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DOOBY.Models;

public partial class CaseStudyContext : DbContext
{
    public CaseStudyContext()
    {
    }

    public CaseStudyContext(DbContextOptions<CaseStudyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Grievance> Grievances { get; set; }

    public virtual DbSet<StationInfo> StationInfos { get; set; }

    public virtual DbSet<StationSelectInfo> StationSelectInfos { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost; database=Case_Study; username=postgres; password=password;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("admins_pkey");

            entity.ToTable("admins");

            entity.Property(e => e.AdminId)
                .ValueGeneratedNever()
                .HasColumnName("admin_id");
            entity.Property(e => e.Fname)
                .HasMaxLength(24)
                .HasColumnName("fname");
            entity.Property(e => e.Lname)
                .HasMaxLength(24)
                .HasColumnName("lname");
            entity.Property(e => e.Permissions)
                .HasColumnType("character varying[]")
                .HasColumnName("permissions");

            entity.HasOne(d => d.AdminNavigation).WithOne(p => p.Admin)
                .HasForeignKey<Admin>(d => d.AdminId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Admins_admin_id");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustId).HasName("customer_pkey");

            entity.ToTable("customer");

            entity.Property(e => e.CustId)
                .ValueGeneratedNever()
                .HasColumnName("cust_id");
            entity.Property(e => e.Fname)
                .HasMaxLength(24)
                .HasColumnName("fname");
            entity.Property(e => e.Lname)
                .HasMaxLength(24)
                .HasColumnName("lname");
            entity.Property(e => e.PhoneNum)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("phone_num");

            entity.HasOne(d => d.Cust).WithOne(p => p.Customer)
                .HasForeignKey<Customer>(d => d.CustId)
                .HasConstraintName("FK_Customer_cust_id");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("feedback_pkey");

            entity.ToTable("feedback");

            entity.Property(e => e.FeedbackId)
                .ValueGeneratedNever()
                .HasColumnName("feedback_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasMaxLength(256)
                .HasColumnName("description");
            entity.Property(e => e.LastEdit).HasColumnName("last_edit");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.StationId).HasColumnName("station_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Feedback_customers.user_id");
        });

        modelBuilder.Entity<Grievance>(entity =>
        {
            entity.HasKey(e => e.GrievanceId).HasName("grievance_pkey");

            entity.ToTable("grievance");

            entity.Property(e => e.GrievanceId)
                .ValueGeneratedNever()
                .HasColumnName("grievance_id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasMaxLength(256)
                .HasColumnName("description");
            entity.Property(e => e.LastEdit).HasColumnName("last_edit");
            entity.Property(e => e.StationId).HasColumnName("station_id");
            entity.Property(e => e.Status)
                .HasMaxLength(24)
                .HasColumnName("status");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Grievances)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Grievance_customers.user_id");
        });

        modelBuilder.Entity<StationInfo>(entity =>
        {
            entity.HasKey(e => e.StationId).HasName("station_info_pkey");

            entity.ToTable("station_info");

            entity.Property(e => e.StationId)
                .ValueGeneratedNever()
                .HasColumnName("station_id");
            entity.Property(e => e.AvailableNodes).HasColumnName("available_nodes");
            entity.Property(e => e.Latitude)
                .HasMaxLength(24)
                .HasColumnName("latitude");
            entity.Property(e => e.Longitude)
                .HasMaxLength(24)
                .HasColumnName("longitude");
            entity.Property(e => e.StationMaster).HasColumnName("station_master");
            entity.Property(e => e.TotalNodes).HasColumnName("total_nodes");

            entity.HasOne(d => d.StationMasterNavigation).WithMany(p => p.StationInfos)
                .HasForeignKey(d => d.StationMaster)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Station_info_admins.station_id");
        });

        modelBuilder.Entity<StationSelectInfo>(entity =>
        {
            entity.HasKey(e => new { e.StationId, e.ProductId }).HasName("station_select_info_pkey");

            entity.ToTable("station_select_info");

            entity.Property(e => e.StationId).HasColumnName("station_id");
            entity.Property(e => e.ProductId)
                .HasMaxLength(24)
                .HasColumnName("product_id");
            entity.Property(e => e.Count)
                .HasDefaultValueSql("0")
                .HasColumnName("count");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.ProductName)
                .HasMaxLength(24)
                .HasColumnName("product_name");

            entity.HasOne(d => d.Station).WithMany(p => p.StationSelectInfos)
                .HasForeignKey(d => d.StationId)
                .HasConstraintName("FK_Station_Select_info.station_id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(24)
                .HasColumnName("password");
            entity.Property(e => e.Type)
                .HasMaxLength(20)
                .HasColumnName("type");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
