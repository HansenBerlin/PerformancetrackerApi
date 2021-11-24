using System;

namespace PerformancetrackerApi.Entities
{
    public class ParticipationWork : Person
    {
        public string Description { get; set; }
        public DateTime CommitDate { get; set; }
        public string Link { get; set; }
    }
}