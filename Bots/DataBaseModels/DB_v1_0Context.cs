using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Bots.DataBaseModels
{
    public partial class DB_v1_0Context : DbContext
    {
        public virtual DbSet<MemberSize> MemberSize { get; set; }
        public virtual DbSet<Province> Province { get; set; }
        public virtual DbSet<Shelter> Shelter { get; set; }
        public virtual DbSet<Town> Town { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=DB_v1.0;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MemberSize>(entity =>
            {
                entity.HasKey(e => e.NumberId);

                entity.ToTable("Member_Size");

                entity.Property(e => e.NumberId)
                    .HasColumnName("Number_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.HasKey(e => e.NumberId);

                entity.Property(e => e.NumberId)
                    .HasColumnName("Number_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Shelter>(entity =>
            {
                entity.HasKey(e => e.NumberId);

                entity.Property(e => e.NumberId)
                    .HasColumnName("Number_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.TownId).HasColumnName("Town_ID");

                entity.HasOne(d => d.Town)
                    .WithMany(p => p.Shelter)
                    .HasForeignKey(d => d.TownId)
                    .HasConstraintName("FK__Shelter__Town_ID__403A8C7D");
            });

            modelBuilder.Entity<Town>(entity =>
            {
                entity.HasKey(e => e.NumberId);

                entity.Property(e => e.NumberId)
                    .HasColumnName("Number_ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ProvinceId).HasColumnName("Province_ID");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Town)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Town__Province_I__3D5E1FD2");
            });
        }
    }
}
