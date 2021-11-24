using System.ComponentModel.DataAnnotations;

namespace PerformancetrackerApi.Entities
{
    public class Student : Person
    {
        public int MatNr { get; set; }
        public string Mail { get; set; }
    }
}