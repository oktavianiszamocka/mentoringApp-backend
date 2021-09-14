namespace MentorApp.DTOs.Responses
{
    public class ProjectMemberDTO
    {
        public int IdUser { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Avatar { get; set; }
        public string ProjectRole { get; set; }
        public string Major { get; set; }
        public int? Semester { get; set; }

    }
}
