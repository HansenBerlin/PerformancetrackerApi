using System;

namespace PerformancetrackerApi.Entities
{
    public class StudentGrade
    {
        public double Value { get; set; }
        public DateTime CommitDate { get; set; }
        public bool Fixed { get; set; }
        public int Id { get; set; }
    }
}