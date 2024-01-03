using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebAppPracticeProject4.Models
{
    public partial class PracticeProject4WebAPIDbContext : DbContext
    {
        public PracticeProject4WebAPIDbContext()
        {
        }

        public PracticeProject4WebAPIDbContext(DbContextOptions<PracticeProject4WebAPIDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Mark> Marks { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=NAVEEN-BOOK-8C9;database=PracticeProject4WebAPIDb;trusted_connection=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Mark>(entity =>
            {
                entity.HasKey(e => e.Mid)
                    .HasName("PK__Marks__C79734EA8F691D4C");

                entity.Property(e => e.Mid)
                    .ValueGeneratedNever()
                    .HasColumnName("MId");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Marks)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__Marks__StudentId__3B75D760");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Marks)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK__Marks__SubjectId__3C69FB99");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.StudentId).ValueGeneratedNever();

                entity.Property(e => e.StudentName).HasMaxLength(50);
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.Property(e => e.SubjectId).ValueGeneratedNever();

                entity.Property(e => e.SubjectName).HasMaxLength(50);

                entity.Property(e => e.TeacherName).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
