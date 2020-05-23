﻿using System;
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
        public virtual DbSet<Meeting> Meeting { get; set; }
        public virtual DbSet<MeetingAttendence> MeetingAttendence { get; set; }
        public virtual DbSet<Message> Message { get; set; }
        public virtual DbSet<Milestone> Milestone { get; set; }
        public virtual DbSet<Note> Note { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<PersonalNote> PersonalNote { get; set; }
        public virtual DbSet<Phase> Phase { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Profile> Profile { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<ProjectHistory> ProjectHistory { get; set; }
        public virtual DbSet<ProjectMembers> ProjectMembers { get; set; }
        public virtual DbSet<ProjectPromoter> ProjectPromoter { get; set; }
        public virtual DbSet<ProjectStatus> ProjectStatus { get; set; }
        public virtual DbSet<Task> Task { get; set; }
        public virtual DbSet<TaskAssigning> TaskAssigning { get; set; }
        public virtual DbSet<Url> Url { get; set; }
        public virtual DbSet<User> User { get; set; }

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

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Comment_User");

                entity.HasOne(d => d.PostNavigation)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.Post)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Comment_Post");
            });

            modelBuilder.Entity<Meeting>(entity =>
            {
                entity.HasKey(e => e.IdMeeting)
                    .HasName("Meeting_pk");

                entity.Property(e => e.IdMeeting).ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.ProjectNavigation)
                    .WithMany(p => p.Meeting)
                    .HasForeignKey(d => d.Project)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Meeting_Project");
            });

            modelBuilder.Entity<MeetingAttendence>(entity =>
            {
                entity.HasKey(e => e.IdAttendence)
                    .HasName("Meeting_attendence_pk");

                entity.ToTable("Meeting_attendence");

                entity.Property(e => e.IdAttendence).ValueGeneratedNever();

                entity.HasOne(d => d.MeetingNavigation)
                    .WithMany(p => p.MeetingAttendence)
                    .HasForeignKey(d => d.Meeting)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Meeting_attendence_Meeting");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.MeetingAttendence)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Meeting_attendence_User");
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

                entity.HasOne(d => d.ReceiverNavigation)
                    .WithMany(p => p.MessageReceiverNavigation)
                    .HasForeignKey(d => d.Receiver)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Receiver_User");

                entity.HasOne(d => d.SenderNavigation)
                    .WithMany(p => p.MessageSenderNavigation)
                    .HasForeignKey(d => d.Sender)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Sender_User");
            });

            modelBuilder.Entity<Milestone>(entity =>
            {
                entity.HasKey(e => e.IdMilestone)
                    .HasName("Milestone_pk");

                entity.Property(e => e.IdMilestone).ValueGeneratedNever();

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.ProjectNavigation)
                    .WithMany(p => p.Milestone)
                    .HasForeignKey(d => d.Project)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Milestone_Project");
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

                entity.HasOne(d => d.AuthorNavigation)
                    .WithMany(p => p.Note)
                    .HasForeignKey(d => d.Author)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Note_User");

                entity.HasOne(d => d.MeetingNavigation)
                    .WithMany(p => p.Note)
                    .HasForeignKey(d => d.Meeting)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Note_Meeting");
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

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.Notification)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Notification_User");
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

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.PersonalNote)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Personal_Note_User");
            });

            modelBuilder.Entity<Phase>(entity =>
            {
                entity.HasKey(e => e.IdPhase)
                    .HasName("Phase_pk");

                entity.Property(e => e.IdPhase).ValueGeneratedNever();

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.ProjectIdProject).HasColumnName("Project_IdProject");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.ProjectIdProjectNavigation)
                    .WithMany(p => p.Phase)
                    .HasForeignKey(d => d.ProjectIdProject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Step_Project");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => e.IdPost)
                    .HasName("Post_pk");

                entity.Property(e => e.IdPost).ValueGeneratedNever();

                entity.Property(e => e.Attachment).HasMaxLength(255);

                entity.Property(e => e.Content)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.DateOfPublication).HasColumnType("date");

                entity.HasOne(d => d.ProjectNavigation)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.Project)
                    .HasConstraintName("Post_Project");

                entity.HasOne(d => d.WriterNavigation)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.Writer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Post_User");
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.HasKey(e => e.IdProfile)
                    .HasName("Profile_pk");

                entity.Property(e => e.IdProfile).ValueGeneratedNever();

                entity.Property(e => e.Experiences)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Major).HasMaxLength(250);

                entity.Property(e => e.Skills)
                    .IsRequired()
                    .HasColumnType("text");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.Profile)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Profile_User");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.IdProject)
                    .HasName("Project_pk");

                entity.Property(e => e.IdProject).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.Icon).IsRequired();

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Project)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Project_Project_Status");

                entity.HasOne(d => d.SuperviserNavigation)
                    .WithMany(p => p.Project)
                    .HasForeignKey(d => d.Superviser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Project_User");
            });

            modelBuilder.Entity<ProjectHistory>(entity =>
            {
                entity.HasKey(e => e.IdHistory)
                    .HasName("Project_History_pk");

                entity.ToTable("Project_History");

                entity.Property(e => e.IdHistory).ValueGeneratedNever();

                entity.Property(e => e.Change)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.WhoChange).HasColumnName("Who_Change");

                entity.HasOne(d => d.ProjectNavigation)
                    .WithMany(p => p.ProjectHistory)
                    .HasForeignKey(d => d.Project)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Project_History_Project");

                entity.HasOne(d => d.WhoChangeNavigation)
                    .WithMany(p => p.ProjectHistory)
                    .HasForeignKey(d => d.WhoChange)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Project_History_User");
            });

            modelBuilder.Entity<ProjectMembers>(entity =>
            {
                entity.HasKey(e => e.IdProjectMember)
                    .HasName("Project_Members_pk");

                entity.ToTable("Project_Members");

                entity.Property(e => e.IdProjectMember)
                    .HasColumnName("IdProject_Member")
                    .ValueGeneratedNever();

                entity.Property(e => e.Role).IsRequired();

                entity.HasOne(d => d.MemberNavigation)
                    .WithMany(p => p.ProjectMembers)
                    .HasForeignKey(d => d.Member)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Project_ProjectMember_User");

                entity.HasOne(d => d.ProjectNavigation)
                    .WithMany(p => p.ProjectMembers)
                    .HasForeignKey(d => d.Project)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Project_ProjectMember_Project");
            });

            modelBuilder.Entity<ProjectPromoter>(entity =>
            {
                entity.HasKey(e => e.IdProjectPromoter)
                    .HasName("Project_Promoter_pk");

                entity.ToTable("Project_Promoter");

                entity.Property(e => e.IdProjectPromoter)
                    .HasColumnName("IdProject_Promoter")
                    .ValueGeneratedNever();

                entity.HasOne(d => d.ProjectNavigation)
                    .WithMany(p => p.ProjectPromoter)
                    .HasForeignKey(d => d.Project)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Project_Promoter_Project");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.ProjectPromoter)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Project_Promoter_User");
            });

            modelBuilder.Entity<ProjectStatus>(entity =>
            {
                entity.HasKey(e => e.IdStatus)
                    .HasName("Project_Status_pk");

                entity.ToTable("Project_Status");

                entity.Property(e => e.IdStatus)
                    .HasColumnName("idStatus")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name).IsRequired();
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

                entity.HasOne(d => d.CreatorNavigation)
                    .WithMany(p => p.Task)
                    .HasForeignKey(d => d.Creator)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Task_User");

                entity.HasOne(d => d.ProjectNavigation)
                    .WithMany(p => p.Task)
                    .HasForeignKey(d => d.Project)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Task_Project");
            });

            modelBuilder.Entity<TaskAssigning>(entity =>
            {
                entity.HasKey(e => e.IdAssign)
                    .HasName("Task_Assigning_pk");

                entity.ToTable("Task_Assigning");

                entity.Property(e => e.IdAssign).ValueGeneratedNever();

                entity.HasOne(d => d.TaskNavigation)
                    .WithMany(p => p.TaskAssigning)
                    .HasForeignKey(d => d.Task)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Task_Assigning_Task");

                entity.HasOne(d => d.UserNavigation)
                    .WithMany(p => p.TaskAssigning)
                    .HasForeignKey(d => d.User)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Task_Assigning_User");
            });

            modelBuilder.Entity<Url>(entity =>
            {
                entity.HasKey(e => e.IdUrl)
                    .HasName("URL_pk");

                entity.ToTable("URL");

                entity.Property(e => e.IdUrl)
                    .HasColumnName("IdURL")
                    .ValueGeneratedNever();

                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Link)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.ProjectNavigation)
                    .WithMany(p => p.Url)
                    .HasForeignKey(d => d.Project)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("URL_Project");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("User_pk");

                entity.Property(e => e.IdUser).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasColumnName("SALT");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}