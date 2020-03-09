using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UploadingFileToDB.DBModels
{
    public partial class FileUploadContext : DbContext
    {
        public FileUploadContext()
        {
        }

        public FileUploadContext(DbContextOptions<FileUploadContext> options)
            : base(options)
        {
        }

        public virtual DbSet<EngagementUpload> EngagementUpload { get; set; }
        public virtual DbSet<EngagementUploadFile> EngagementUploadFile { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=NITTUJULI\\SQLEXPRESS;Database=FileUpload;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EngagementUpload>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FileUploadId).HasColumnName("FileUploadID");

                entity.Property(e => e.OpportunityId).HasColumnName("OpportunityID");

                entity.HasOne(d => d.FileUpload)
                    .WithMany(p => p.EngagementUpload)
                    .HasForeignKey(d => d.FileUploadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EngagementUpload_EngagementUploadFile");
            });

            modelBuilder.Entity<EngagementUploadFile>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FileContentType).HasMaxLength(100);

                entity.Property(e => e.FileName)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.UploadedBy)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.UploadedDate).HasColumnType("datetime");

                entity.Property(e => e.UploadedFile).IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
