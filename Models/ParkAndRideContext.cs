using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Web;

namespace ParkAndRide.Models
{
    public partial class ParkAndRideContext : DbContext
    {
        public ParkAndRideContext()
        {
        }

        public ParkAndRideContext(DbContextOptions<ParkAndRideContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<AdditionInfo> AdditionInfo { get; set; }
        public virtual DbSet<Card> Card { get; set; }
        public virtual DbSet<Parking> Parking { get; set; }
        public virtual DbSet<ParkingAdministrator> ParkingAdministrator { get; set; }
        public virtual DbSet<ParkingPlace> ParkingPlace { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-0348EAM\\SQLEXPRESS;Database=ParkAndRide;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasIndex(e => e.UserId)
                    .HasName("IXFK_Konto_Uzytkownik");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Blocked).HasColumnName("blocked");

                entity.Property(e => e.CreationDate)
                    .HasColumnName("creation_date")
                    .HasColumnType("date");

                entity.Property(e => e.IsActive).HasColumnName("is_active");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ReasonForBlocking)
                    .HasColumnName("reason_for_blocking")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Konto_Uzytkownik");
            });

            modelBuilder.Entity<AdditionInfo>(entity =>
            {
                entity.HasKey(e => e.InfoId);

                entity.ToTable("Addition_info");

                entity.HasIndex(e => e.UserId)
                    .HasName("IXFK_Dodatkowe_informacje_Uzytkownik");

                entity.Property(e => e.InfoId).HasColumnName("info_id");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.TypeOfMachine)
                    .IsRequired()
                    .HasColumnName("type_of_machine")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Width).HasColumnName("width");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AdditionInfo)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Dodatkowe_informacje_Uzytkownik");
            });

            modelBuilder.Entity<Card>(entity =>
            {
                entity.HasIndex(e => e.ParkingId)
                    .HasName("IXFK_Karta_pobytu_Parking");

                entity.HasIndex(e => e.UserId)
                    .HasName("IXFK_Karta_pobytu_Uzytkownik");

                entity.Property(e => e.CardId).HasColumnName("card_id");

                entity.Property(e => e.DataFrom)
                    .HasColumnName("data_from")
                    .HasColumnType("datetime");

                entity.Property(e => e.DataTo)
                    .HasColumnName("data_to")
                    .HasColumnType("datetime");

                entity.Property(e => e.ParkingId).HasColumnName("parking_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Parking)
                    .WithMany(p => p.Card)
                    .HasForeignKey(d => d.ParkingId)
                    .HasConstraintName("FK_Karta_pobytu_Parking");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Card)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Karta_pobytu_Uzytkownik");
            });

            modelBuilder.Entity<Parking>(entity =>
            {
                entity.HasIndex(e => e.AdminId)
                    .HasName("IXFK_Parking_Zarzadca");

                entity.Property(e => e.ParkingId).HasColumnName("parking_id");

                entity.Property(e => e.AdminId).HasColumnName("admin_id");

                entity.Property(e => e.Area).HasColumnName("area");

                entity.Property(e => e.CreationDate)
                    .HasColumnName("creation_date")
                    .HasColumnType("date");

                entity.Property(e => e.Guarded).HasColumnName("guarded");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasColumnName("number")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.NumberOfFloor).HasColumnName("number_of_floor");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Place)
                    .IsRequired()
                    .HasColumnName("place")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostCode)
                    .HasColumnName("post_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasColumnName("street")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UndergroundPlace)
                    .HasColumnName("underground_place")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.Parking)
                    .HasForeignKey(d => d.AdminId)
                    .HasConstraintName("FK_Parking_Zarzadca");
                entity.Property(e => e.GpsLat)
                    .HasColumnName("gps_lat")
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.GpsLng)
                    .HasColumnName("gps_lng")
                    .HasMaxLength(50)
                    .IsUnicode(false);

            });

            modelBuilder.Entity<ParkingAdministrator>(entity =>
            {
                entity.HasKey(e => e.AdminId);

                entity.ToTable("Parking_administrator");

                entity.Property(e => e.AdminId).HasColumnName("admin_id");

                entity.Property(e => e.EMail)
                    .IsRequired()
                    .HasColumnName("e-mail")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Nip)
                    .IsRequired()
                    .HasColumnName("NIP")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.NumberOfBuilding)
                    .IsRequired()
                    .HasColumnName("number_of_building")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.NumberOfFlat)
                    .HasColumnName("number_of_flat")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Place)
                    .IsRequired()
                    .HasColumnName("place")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostCode)
                    .IsRequired()
                    .HasColumnName("post_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Regon)
                    .HasColumnName("REGON")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasColumnName("street")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ParkingPlace>(entity =>
            {
                entity.HasKey(e => e.PlaceId);

                entity.ToTable("Parking_place");

                entity.HasIndex(e => e.ParkingId)
                    .HasName("IXFK_Miejsce_parkingowe_Parking");

                entity.Property(e => e.PlaceId).HasColumnName("place_id");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.Length).HasColumnName("length");

                entity.Property(e => e.ParkingId).HasColumnName("parking_id");

                entity.Property(e => e.TypeOfPlace)
                    .IsRequired()
                    .HasColumnName("type_of_place")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Width).HasColumnName("width");

                entity.HasOne(d => d.Parking)
                    .WithMany(p => p.ParkingPlace)
                    .HasForeignKey(d => d.ParkingId)
                    .HasConstraintName("FK_Miejsce_parkingowe_Parking");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Birthday)
                    .HasColumnName("birthday")
                    .HasColumnType("date");

                entity.Property(e => e.EMail)
                    .IsRequired()
                    .HasColumnName("e-mail")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NumberOfBuilding)
                    .IsRequired()
                    .HasColumnName("number_of_building")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.NumberOfFlat)
                    .IsRequired()
                    .HasColumnName("number_of_flat")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Pesel).HasColumnName("PESEL");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasColumnName("phone")
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Place)
                    .IsRequired()
                    .HasColumnName("place")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostCode)
                    .IsRequired()
                    .HasColumnName("post_code")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sex)
                    .IsRequired()
                    .HasColumnName("sex")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasColumnName("street")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasMaxLength(50)
                    .IsUnicode(false);

            });

        }
    }
}
