using System;

namespace PerformancetrackerApi.Entities
{
    public class Dashboard
    {
        public int MatNr { get; set; }
        public int LatedaysUsed { get; set; }
        public int Penaltys { get; set; }
        public int WorksCommited { get; set; }
        public int WorksOpen { get; set; }
        public double AggregatedGrade { get; set; }
        public DateTime NextDueDate { get; set; }
    }
}