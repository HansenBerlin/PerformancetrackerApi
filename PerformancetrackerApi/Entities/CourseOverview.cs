using System.ComponentModel.DataAnnotations;

namespace PerformancetrackerApi.Entities
{
    public class CourseOverview
    {
        public int Id { get; set; }
        public string ProfMail { get; set; }
        public string Fullname { get; set; }
        public string ShortDescription { get; set; }
        public string DescriptionCourse { get; set; }
        public string DescriptionModule { get; set; }
    }
}