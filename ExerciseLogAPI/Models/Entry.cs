using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ExerciseLogAPI.Models
{
    public class Entry
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public Exercise ExerciseType { get; set; }
        public int Duration { get; set; }
    }
}
