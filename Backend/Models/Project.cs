using System;
using System.Collections.Generic;

namespace MentorApp.Models
{
    public partial class Project
    {
        public Project()
        {
            Meeting = new HashSet<Meeting>();
            Milestone = new HashSet<Milestone>();
            Post = new HashSet<Post>();
            ProjectMembers = new HashSet<ProjectMembers>();
            ProjectPromoter = new HashSet<ProjectPromoter>();
            Task = new HashSet<Task>();
            Url = new HashSet<Url>();
            Invitation = new HashSet<Invitation>();
        }

        public int IdProject { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? Superviser { get; set; }
        public string Icon { get; set; }
        public int Status { get; set; }
        public int? Studies { get; set; }
        public int? Mode { get; set; }

        public virtual ProjectStatus StatusNavigation { get; set; }
        public virtual User SuperviserNavigation { get; set; }
        public virtual ICollection<Meeting> Meeting { get; set; }
        public virtual ICollection<Milestone> Milestone { get; set; }
        public virtual ICollection<Post> Post { get; set; }
        public virtual ICollection<ProjectMembers> ProjectMembers { get; set; }
        public virtual ICollection<ProjectPromoter> ProjectPromoter { get; set; }
        public virtual ICollection<Task> Task { get; set; }
        public virtual ICollection<Url> Url { get; set; }
        public virtual ICollection<Invitation> Invitation { get; set; }
        public virtual ProjectStudies StudiesNavigation { get; set; }
        public virtual ProjectMode ModeNavigation { get; set; }
    }
}
