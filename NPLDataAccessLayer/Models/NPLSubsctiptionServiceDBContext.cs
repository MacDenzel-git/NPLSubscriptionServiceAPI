using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NPLDataAccessLayer.Models
{
    public partial class NPLSubsctiptionServiceDBContext : DbContext
    {
        public NPLSubsctiptionServiceDBContext()
        {
        }

        public NPLSubsctiptionServiceDBContext(DbContextOptions<NPLSubsctiptionServiceDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Branch> Branches { get; set; } = null!;
        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<ClientType> ClientTypes { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<District> Districts { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<PaymentType> PaymentTypes { get; set; } = null!;
        public virtual DbSet<Promotion> Promotions { get; set; } = null!;
        public virtual DbSet<Region> Regions { get; set; } = null!;
        public virtual DbSet<Subscription> Subscriptions { get; set; } = null!;
        public virtual DbSet<SubscriptionStatus> SubscriptionStatuses { get; set; } = null!;
        public virtual DbSet<SubscriptionType> SubscriptionTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Database=NPLSubsctiptionServiceDB; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Branch>(entity =>
            {
                entity.ToTable("Branch");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.BranchName).HasMaxLength(50);

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Branches)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Branch_Country");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.ClientName).HasMaxLength(100);

                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Location).HasMaxLength(50);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PhoneNumber).HasMaxLength(50);
                entity.Property(e => e.Email).HasMaxLength(50);

                entity.HasOne(d => d.ClientType)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.ClientTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Clients_ClientType");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Clients_Districts");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Clients_Regions");
            });

            modelBuilder.Entity<ClientType>(entity =>
            {
                entity.ToTable("ClientType");

                entity.Property(e => e.Description).HasMaxLength(50);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CountryName).HasMaxLength(10);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.Property(e => e.DistrictId).HasColumnName("DistrictID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.DistrictName).HasMaxLength(50);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Districts_Countries");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.PaymentType)
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            modelBuilder.Entity<PaymentType>(entity =>
            {
                entity.ToTable("PaymentType");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.Property(e => e.PromotionId).ValueGeneratedNever();

                entity.Property(e => e.PromotionCode).HasMaxLength(50);
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.Property(e => e.RegionId).HasColumnName("RegionID");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.RegionName).HasMaxLength(50);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Regions)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Regions_Countries");
            });

            modelBuilder.Entity<Subscription>(entity =>
            {
                entity.Property(e => e.ChargeInMwk)
                    .HasColumnType("decimal(19, 4)")
                    .HasColumnName("ChargeInMWK");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DateOfSubscription).HasColumnType("datetime");

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Subscriptions_Clients");

                entity.HasOne(d => d.Payment)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.PaymentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Subscriptions_Payments");

                entity.HasOne(d => d.Promotion)
                    .WithMany(p => p.Subscriptions)
                    .HasForeignKey(d => d.PromotionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Subscriptions_Promotions");
            });

            modelBuilder.Entity<SubscriptionStatus>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(10)
                    .IsFixedLength();

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<SubscriptionType>(entity =>
            {
                entity.ToTable("SubscriptionType");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.SubscriptionFee).HasColumnType("decimal(19, 4)");

                entity.Property(e => e.SubscriptionName).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
