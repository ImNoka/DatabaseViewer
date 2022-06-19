using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataBaseManager.Model
{
    public partial class test_dbContext : DbContext
    {
        string ConnectionString;
        public test_dbContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public test_dbContext(DbContextOptions<test_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PipingFluid> PipingFluids { get; set; } = null!;
        public virtual DbSet<PipingPhysical> PipingPhysicals { get; set; } = null!;
        public virtual DbSet<PipingRun> PipingRuns { get; set; } = null!;
        public virtual DbSet<RunToFluid> RunToFluids { get; set; } = null!;
        public virtual DbSet<RunToPhysical> RunToPhysicals { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlite("DataSource=C:\\Projects\\Csh\\К собеседованию дотнет\\Нефтехимпроект\\test_db.db3");
                optionsBuilder.UseSqlite($"DataSource={ConnectionString}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PipingFluid>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PipingFluid");

                entity.HasIndex(e => e.Oid, "IX_PipingFluid_OID")
                    .IsUnique();

                entity.Property(e => e.FluidCode).HasColumnType("CHAR (2)");

                entity.Property(e => e.Oid)
                    .HasColumnType("STRING (226)")
                    .HasColumnName("OID");

                entity.Property(e => e.PressureRating).HasColumnType("DOUBLE");

                entity.Property(e => e.Temp).HasColumnType("DOUBLE");
            });

            modelBuilder.Entity<PipingPhysical>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PipingPhysical");

                entity.HasIndex(e => e.Oid, "IX_PipingPhysical_OID")
                    .IsUnique();

                entity.Property(e => e.LineWeight).HasColumnType("DOUBLE");

                entity.Property(e => e.Oid)
                    .HasColumnType("STRING (226)")
                    .HasColumnName("OID");

                entity.Property(e => e.RunDiam).HasColumnType("DOUBLE");

                entity.Property(e => e.RunLength).HasColumnType("DOUBLE");

                entity.Property(e => e.WallThickness).HasColumnType("DOUBLE");
            });

            modelBuilder.Entity<PipingRun>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PipingRun");

                entity.HasIndex(e => e.Oid, "IX_PipingRun_OID")
                    .IsUnique();

                entity.Property(e => e.ItemTag).HasColumnType("STRING (128)");

                entity.Property(e => e.Npd).HasColumnName("NPD");

                entity.Property(e => e.Oid)
                    .HasColumnType("STRING (226)")
                    .HasColumnName("OID");

                entity.Property(e => e.RunName).HasColumnType("STRING (256)");
            });

            modelBuilder.Entity<RunToFluid>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("RunToFluid");

                entity.Property(e => e.Oidfrom)
                    .HasColumnType("STRING (226)")
                    .HasColumnName("OIDFrom");

                entity.Property(e => e.Oidto)
                    .HasColumnType("STRING (226)")
                    .HasColumnName("OIDTo");
            });

            modelBuilder.Entity<RunToPhysical>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("RunToPhysical");

                entity.Property(e => e.Oidfrom)
                    .HasColumnType("STRING (226)")
                    .HasColumnName("OIDFrom");

                entity.Property(e => e.Oidto)
                    .HasColumnType("STRING (226)")
                    .HasColumnName("OIDTo");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
