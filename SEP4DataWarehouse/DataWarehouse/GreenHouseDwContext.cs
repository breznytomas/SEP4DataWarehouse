using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Npgsql;

namespace SEP4DataWarehouse.DataWarehouseModels
{
    public partial class GreenHouseDwContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public GreenHouseDwContext()
        {
        }

        public GreenHouseDwContext(DbContextOptions<GreenHouseDwContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dimboard> Dimboards { get; set; } = null!;
        public virtual DbSet<Dimcarbondioxide> Dimcarbondioxides { get; set; } = null!;
        public virtual DbSet<Dimdate> Dimdates { get; set; } = null!;
        public virtual DbSet<Dimhumidity> Dimhumidities { get; set; } = null!;
        public virtual DbSet<Dimlight> Dimlights { get; set; } = null!;
        public virtual DbSet<Dimtemperature> Dimtemperatures { get; set; } = null!;
        public virtual DbSet<Dimuser> Dimusers { get; set; } = null!;
        public virtual DbSet<Factmeasurement> Factmeasurements { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseNpgsql(getDatabaseUrl());

        private String getDatabaseUrl()
        {
            string? databaseUrl;
            #if (DEBUG)
            databaseUrl = System.IO.File.ReadAllText("./DataWarehouse/DwString.txt");
            #else
            // for heroku
            databaseUrl = Environment.GetEnvironmentVariable("AMAZON_DB");
            #endif
            return databaseUrl;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("postgres_fdw");

            modelBuilder.Entity<Dimboard>(entity =>
            {
                entity.HasKey(e => e.BId)
                    .HasName("pk_dimboard");

                entity.ToTable("dimboard", "edw");

                entity.Property(e => e.BId).HasColumnName("b_id");

                entity.Property(e => e.BoardId)
                    .HasMaxLength(100)
                    .HasColumnName("board_id");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Validfrom).HasColumnName("validfrom");

                entity.Property(e => e.Validto).HasColumnName("validto");
            });

            modelBuilder.Entity<Dimcarbondioxide>(entity =>
            {
                entity.HasKey(e => e.CdId)
                    .HasName("pk_dimcarbondioxide");

                entity.ToTable("dimcarbondioxide", "edw");

                entity.Property(e => e.CdId).HasColumnName("cd_id");

                entity.Property(e => e.Carbondioxideid).HasColumnName("carbondioxideid");

                entity.Property(e => e.Istop)
                    .HasMaxLength(100)
                    .HasColumnName("istop");

                entity.Property(e => e.Lowerlimit).HasColumnName("lowerlimit");

                entity.Property(e => e.Upperlimit).HasColumnName("upperlimit");

                entity.Property(e => e.Validfrom).HasColumnName("validfrom");

                entity.Property(e => e.Validto).HasColumnName("validto");

                entity.Property(e => e.Wastriggered)
                    .HasMaxLength(50)
                    .HasColumnName("wastriggered");
            });

            modelBuilder.Entity<Dimdate>(entity =>
            {
                entity.HasKey(e => e.DId)
                    .HasName("pk_dimdate");

                entity.ToTable("dimdate", "edw");

                entity.Property(e => e.DId).HasColumnName("d_id");

                entity.Property(e => e.Day).HasColumnName(" Day");

                entity.Property(e => e.DayName).HasMaxLength(25);

                entity.Property(e => e.Monthname)
                    .HasMaxLength(25)
                    .HasColumnName("monthname");
            });

            modelBuilder.Entity<Dimhumidity>(entity =>
            {
                entity.HasKey(e => e.HId)
                    .HasName("pk_dimhumidity");

                entity.ToTable("dimhumidity", "edw");

                entity.Property(e => e.HId).HasColumnName("h_id");

                entity.Property(e => e.Humidityid).HasColumnName("humidityid");

                entity.Property(e => e.Istop)
                    .HasMaxLength(100)
                    .HasColumnName("istop");

                entity.Property(e => e.Lowerlimit).HasColumnName("lowerlimit");

                entity.Property(e => e.Upperlimit).HasColumnName("upperlimit");

                entity.Property(e => e.Validfrom).HasColumnName("validfrom");

                entity.Property(e => e.Validto).HasColumnName("validto");

                entity.Property(e => e.Wastriggered)
                    .HasMaxLength(50)
                    .HasColumnName("wastriggered");
            });

            modelBuilder.Entity<Dimlight>(entity =>
            {
                entity.HasKey(e => e.LId)
                    .HasName("pk_dimlight");

                entity.ToTable("dimlight", "edw");

                entity.Property(e => e.LId).HasColumnName("l_id");

                entity.Property(e => e.Istop)
                    .HasMaxLength(100)
                    .HasColumnName("istop");

                entity.Property(e => e.Lightid).HasColumnName("lightid");

                entity.Property(e => e.Lowerlimit).HasColumnName("lowerlimit");

                entity.Property(e => e.Upperlimit).HasColumnName("upperlimit");

                entity.Property(e => e.Validfrom).HasColumnName("validfrom");

                entity.Property(e => e.Validto).HasColumnName("validto");

                entity.Property(e => e.Wastriggered)
                    .HasMaxLength(50)
                    .HasColumnName("wastriggered");
            });

            modelBuilder.Entity<Dimtemperature>(entity =>
            {
                entity.HasKey(e => e.TId)
                    .HasName("pk_dimtemperature");

                entity.ToTable("dimtemperature", "edw");

                entity.Property(e => e.TId).HasColumnName("t_id");

                entity.Property(e => e.Istop)
                    .HasMaxLength(100)
                    .HasColumnName("istop");

                entity.Property(e => e.Lowerlimit).HasColumnName("lowerlimit");

                entity.Property(e => e.Temperatureid).HasColumnName("temperatureid");

                entity.Property(e => e.Upperlimit).HasColumnName("upperlimit");

                entity.Property(e => e.Validfrom).HasColumnName("validfrom");

                entity.Property(e => e.Validto).HasColumnName("validto");

                entity.Property(e => e.Wastriggered)
                    .HasMaxLength(50)
                    .HasColumnName("wastriggered");
            });

            modelBuilder.Entity<Dimuser>(entity =>
            {
                entity.HasKey(e => e.UId)
                    .HasName("pk_dimuser");

                entity.ToTable("dimuser", "edw");

                entity.Property(e => e.UId).HasColumnName("u_id");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Validfrom).HasColumnName("validfrom");

                entity.Property(e => e.Validto).HasColumnName("validto");
            });

            modelBuilder.Entity<Factmeasurement>(entity =>
            {
                entity.HasKey(e => new { e.Measurementid, e.TId, e.LId, e.HId, e.BId, e.CdId, e.DId })
                    .HasName("pk_factmeasurement");

                entity.ToTable("factmeasurement", "edw");

                entity.Property(e => e.Measurementid)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("measurementid");

                entity.Property(e => e.TId).HasColumnName("t_id");

                entity.Property(e => e.LId).HasColumnName("l_id");

                entity.Property(e => e.HId).HasColumnName("h_id");

                entity.Property(e => e.BId).HasColumnName("b_id");

                entity.Property(e => e.CdId).HasColumnName("cd_id");

                entity.Property(e => e.DId).HasColumnName("d_id");

                entity.Property(e => e.Carbondioxidevalue).HasColumnName("carbondioxidevalue");

                entity.Property(e => e.Humidityvalue).HasColumnName("humidityvalue");

                entity.Property(e => e.Lightvalue).HasColumnName("lightvalue");

                entity.Property(e => e.Temperaturevalue).HasColumnName("temperaturevalue");

                entity.HasOne(d => d.BIdNavigation)
                    .WithMany(p => p.Factmeasurements)
                    .HasForeignKey(d => d.BId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_factmeasurement_3");

                entity.HasOne(d => d.Cd)
                    .WithMany(p => p.Factmeasurements)
                    .HasForeignKey(d => d.CdId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_factmeasurement_4");

                entity.HasOne(d => d.DIdNavigation)
                    .WithMany(p => p.Factmeasurements)
                    .HasForeignKey(d => d.DId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_factmeasurement_5");

                entity.HasOne(d => d.HIdNavigation)
                    .WithMany(p => p.Factmeasurements)
                    .HasForeignKey(d => d.HId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_factmeasurement_2");

                entity.HasOne(d => d.LIdNavigation)
                    .WithMany(p => p.Factmeasurements)
                    .HasForeignKey(d => d.LId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_factmeasurement_1");

                entity.HasOne(d => d.TIdNavigation)
                    .WithMany(p => p.Factmeasurements)
                    .HasForeignKey(d => d.TId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_factmeasurement_0");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
