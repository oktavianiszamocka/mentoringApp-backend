using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class User
    {
        public User()
        {
            MeetingAttendence = new HashSet<MeetingAttendence>();
            MessageReceiverNavigation = new HashSet<Message>();
            MessageSenderNavigation = new HashSet<Message>();
            Note = new HashSet<Note>();
            Notification = new HashSet<Notification>();
            PersonalNote = new HashSet<PersonalNote>();
            Post = new HashSet<Post>();
            Profile = new HashSet<Profile>();
            Project = new HashSet<Project>();
            ProjectHistory = new HashSet<ProjectHistory>();
            ProjectMembers = new HashSet<ProjectMembers>();
            ProjectPromoter = new HashSet<ProjectPromoter>();
            Task = new HashSet<Task>();
            TaskAssigning = new HashSet<TaskAssigning>();
        }

        public int IdUser { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }

        public virtual ICollection<MeetingAttendence> MeetingAttendence { get; set; }
        public virtual ICollection<Message> MessageReceiverNavigation { get; set; }
        public virtual ICollection<Message> MessageSenderNavigation { get; set; }
        public virtual ICollection<Note> Note { get; set; }
        public virtual ICollection<Notification> Notification { get; set; }
        public virtual ICollection<PersonalNote> PersonalNote { get; set; }
        public virtual ICollection<Post> Post { get; set; }
        public virtual ICollection<Profile> Profile { get; set; }
        public virtual ICollection<Project> Project { get; set; }
        public virtual ICollection<ProjectHistory> ProjectHistory { get; set; }
        public virtual ICollection<ProjectMembers> ProjectMembers { get; set; }
        public virtual ICollection<ProjectPromoter> ProjectPromoter { get; set; }
        public virtual ICollection<Task> Task { get; set; }
        public virtual ICollection<TaskAssigning> TaskAssigning { get; set; }
    }
}
