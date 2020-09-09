using System;
using System.Collections.Generic;
using System.Linq;
using ExerciseLogAPI.Models;

namespace ExerciseLogAPI.Repositoiries
{
    public class EntriesRepo
    {
        public static EntriesRepo Inst { get; } = new EntriesRepo();

        private List<Entry> entries;
        private int nextID => entries.Count;

        private EntriesRepo()
        {
            entries = new List<Entry>();
            AddDummyEntries();
        }

        private void AddDummyEntries()
        {
            AddEntry(Exercise.Walking, 60 * 60 * 1000);
            AddEntry(Exercise.Running, 10 * 60 * 1000);
            AddEntry(Exercise.Tennis, 55 * 60 * 1000);
        }

        public List<Entry> GetEntries()
        {
            return entries;
        }

        public void AddEntry(Entry entry)
        {
            entry.ID = nextID;
            entries.Add(entry);
        }

        public void AddEntry(Exercise exercise, int duration)
        {
            Entry entry = new Entry
            {
                ExerciseType = exercise,
                Date = DateTime.Now,
                Duration = duration
            };

            AddEntry(entry);
        }

        public Entry GetEntry(int id)
        {
            try
            {
                return entries.First(e => e.ID == id);
            }

            catch (Exception e)
            {
                // TODO: logging?
                Console.WriteLine("Invalid ID requested in EntriesRepo.GetEntry(int id)");
            }

            return null;
        }

        public List<Entry> GetEntriesByExercise(Exercise exercise)
        {
            var query = entries.Where(x => x.ExerciseType == exercise);
            return query.ToList(); // Does this throw exception on null?
        }
    }
}
