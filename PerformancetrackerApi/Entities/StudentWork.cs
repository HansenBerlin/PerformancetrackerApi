using System;

namespace PerformancetrackerApi.Entities
{
    public class StudentWork
    {
        public int Id { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime CommitDate { get; set; }
        public int UsedLatedays { get; set; }
        public double Grade { get; set; }
        public string Type { get; set; }
        public bool Fixed { get; set; }
        public int LeftLatedays { get; set; }
        public int FkCourse { get; set; }

    }
}