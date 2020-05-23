using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Backend.Models
{
    public partial class s17874Context : DbContext
    {
        public s17874Context()
        {
        }

        public s17874Context(DbContextOptions<s17874Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Note> Note { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<PersonalNote> PersonalNote { get; set; }
        public virtual DbSet<Task> Task { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=db-mssql;Initial Catalog=s17874;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.IdComment)
                    .HasName("Comment_pk");

                entity.Property(e => e.IdComment).ValueGeneratedNever();

                entity.Property(e => e.Comment1)
                    .IsRequired()
                    .HasColumnName("Comment")
                    .HasColumnType("text");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.IdMessage)
                    .HasName("Message_pk");

                entity.Property(e => e.IdMessage).ValueGeneratedNever();

                entity.Property(e => e.Attachment).HasMaxLength(255);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Message1)
                    .IsRequired()
                    .HasColumnName("Message")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.HasKey(e => e.IdNote)
                    .HasName("Note_pk");

                entity.Property(e => e.IdNote).ValueGeneratedNever();

                entity.Property(e => e.Attachments).HasMaxLength(255);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Note1)
                    .IsRequired()
                    .HasColumnName("Note")
                    .HasColumnType("text");

                entity.Property(e => e.Subject)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.HasKey(e => e.IdNotification)
                    .HasName("Notification_pk");

                entity.Property(e => e.IdNotification).ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Notification1)
                    .IsRequired()
                    .HasColumnName("Notification")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<PersonalNote>(entity =>
            {
                entity.HasKey(e => e.IdNote)
                    .HasName("Personal_Note_pk");

                entity.ToTable("Personal_Note");

                entity.Property(e => e.IdNote).ValueGeneratedNever();

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Task>(entity =>
            {
                entity.HasKey(e => e.IdTask)
                    .HasName("Task_pk");

                entity.Property(e => e.IdTask).ValueGeneratedNever();

                entity.Property(e => e.ActualEndDate).HasColumnType("date");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.ExpectedEndDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
