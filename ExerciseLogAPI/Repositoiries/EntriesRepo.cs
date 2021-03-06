﻿using System;
using System.Collections.Generic;
using System.Linq;
using ExerciseLogAPI.Models;
using Microsoft.Extensions.Logging;

namespace ExerciseLogAPI.Repositoiries
{
    public class EntriesRepo
    {
        private readonly ILogger<EntriesRepo> _logger;
        private List<Entry> entries;
        private int NextID => entries.Count;

        public EntriesRepo(ILogger<EntriesRepo> logger)
        {
            _logger = logger;
            entries = new List<Entry>();
            AddDummyEntries();
        }

        private void AddDummyEntries()
        {
            AddEntry(Exercise.Walking, 60 * 60 * 1000);
            AddEntry(Exercise.Running, 10 * 60 * 1000);
            AddEntry(Exercise.Tennis, 55 * 60 * 1000);
            AddEntry(Exercise.Tennis, 55 * 2 * 60 * 1000);
        }

        public List<Entry> GetEntries()
        {
            return entries;
        }

        public void AddEntry(Entry entry)
        {
            entry.ID = NextID;
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
                _logger.LogError("Invalid ID requested in EntriesRepo.GetEntry(int id)", id, e);
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
