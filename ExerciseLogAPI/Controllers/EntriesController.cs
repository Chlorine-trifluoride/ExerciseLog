using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ExerciseLogAPI.Models;
using ExerciseLogAPI.Repositoiries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ExerciseLogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntriesController : ControllerBase
    {
        private readonly ILogger<EntriesController> _logger;
        private readonly EntriesRepo _entriesRepo;

        public EntriesController(ILogger<EntriesController> logger, EntriesRepo entriesRepo)
        {
            _logger = logger;
            _entriesRepo = entriesRepo;
        }

        [HttpGet]
        public List<Entry> Get()
        {
            return _entriesRepo.GetEntries();
        }

        [HttpGet("{id:int}")]
        public Entry GetEntryByID(int id)
        {
            return _entriesRepo.GetEntry(id);
        }

        [HttpGet("{exerciseType}")]
        public ActionResult GetEntriesOfType(string exerciseType)
        {
            try
            {
                Exercise exercise = (Exercise)Enum.Parse(typeof(Exercise), exerciseType, true);
                return Ok(_entriesRepo.GetEntriesByExercise(exercise));
            }

            catch (Exception e)
            {
                _logger.LogWarning("Invalid Exercise requested", exerciseType, e.Message);
                return BadRequest();
            }
        }

        [HttpPost()]
        public HttpStatusCode Post([FromBody] Entry entry)
        {
            if (entry.ExerciseType == Exercise.Golf)
            {
                // Golf is not real exercise
                return HttpStatusCode.NotAcceptable;
            }

            else
            {
                _entriesRepo.AddEntry(entry);
                return HttpStatusCode.OK;
            }
        }
    }
}
