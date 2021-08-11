using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Event.API.Event.DAL.DB
{
    public partial class eventdbContext : DbContext
    {
        public eventdbContext()
        {
        }

        public eventdbContext(DbContextOptions<eventdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Tournament> Tournaments { get; set; }
        public virtual DbSet<TournamentTranslate> TournamentTranslates { get; set; }
        public virtual DbSet<TournamentUser> TournamentUsers { get; set; }
        public virtual DbSet<Type> Types { get; set; }
        public virtual DbSet<TypeTranslate> TypeTranslates { get; set; }
        public virtual DbSet<Winning> Winnings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3307;database=eventdb;user id=root;password=1a456#idgj_5f@sj*du7fg78@;treattinyasboolean=false", Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.22-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            modelBuilder.Entity<Tournament>(entity =>
            {
                entity.ToTable("tournament");

                entity.HasIndex(e => e.TypeId, "Fk_Tournament_Type_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ID");

                entity.Property(e => e.ActionTypeId).HasMaxLength(150);

                entity.Property(e => e.Code).HasMaxLength(150);

                entity.Property(e => e.CreatedBy).HasMaxLength(150);

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Description).HasMaxLength(150);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.ImageUrl).HasMaxLength(150);

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.LeaderBoardId).HasMaxLength(150);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(150);

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.NumberOfParticipants).HasColumnType("bigint(20)");

                entity.Property(e => e.ObjectId).HasMaxLength(150);

                entity.Property(e => e.ObjectTypeId).HasMaxLength(150);

                entity.Property(e => e.PriceId).HasMaxLength(150);

                entity.Property(e => e.Public)
                    .IsRequired()
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.TypeId).HasColumnType("bigint(20)");

                entity.Property(e => e.UserHostId).HasMaxLength(150);

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Tournaments)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("Fk_Tournament_Type");
            });

            modelBuilder.Entity<TournamentTranslate>(entity =>
            {
                entity.ToTable("tournament_translate");

                entity.HasIndex(e => e.TournamentId, "Fk_tournament_translate_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasMaxLength(150);

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Description).HasMaxLength(150);

                entity.Property(e => e.ImageUrl).HasMaxLength(150);

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.LanguageId)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(150);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.TournamentId).HasColumnType("bigint(20)");

                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.TournamentTranslates)
                    .HasForeignKey(d => d.TournamentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Fk_tournament_translate");
            });

            modelBuilder.Entity<TournamentUser>(entity =>
            {
                entity.ToTable("tournament_user");

                entity.HasIndex(e => e.TournamentId, "Fk_user_Tournament_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasMaxLength(150);

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(150);

                entity.Property(e => e.TournamentId).HasColumnType("bigint(20)");

                entity.Property(e => e.UserId).HasMaxLength(150);

                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.TournamentUsers)
                    .HasForeignKey(d => d.TournamentId)
                    .HasConstraintName("Fk_user_Tournament");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.ToTable("type");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasMaxLength(150);

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(150);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<TypeTranslate>(entity =>
            {
                entity.ToTable("type_translate");

                entity.HasIndex(e => e.TypeId, "FK_Type_Translate_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ID");

                entity.Property(e => e.CreatedBy).HasMaxLength(150);

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.LanguageId)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasMaxLength(150);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.TypeId).HasColumnType("bigint(20)");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.TypeTranslates)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Type_Translate");
            });

            modelBuilder.Entity<Winning>(entity =>
            {
                entity.ToTable("winning");

                entity.HasIndex(e => e.TournamentId, "Fk_Winning_Tournament_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("ID");

                entity.Property(e => e.Amount)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.CreatedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy).HasColumnType("bigint(20)");

                entity.Property(e => e.Order)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("order");

                entity.Property(e => e.PriceTypeId).HasMaxLength(150);

                entity.Property(e => e.TournamentId).HasColumnType("bigint(20)");

                entity.HasOne(d => d.Tournament)
                    .WithMany(p => p.Winnings)
                    .HasForeignKey(d => d.TournamentId)
                    .HasConstraintName("Fk_Winning_Tournament");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
