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

        public EntriesController(ILogger<EntriesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Entry> Get()
        {
            return EntriesRepo.Inst.GetEntries();
        }

        [HttpGet("{id:int}")]
        public Entry GetEntryByID(int id)
        {
            return EntriesRepo.Inst.GetEntry(id);
        }

        [HttpGet("{exerciseType}")]
        public IActionResult GetEntriesOfType(string exerciseType)
        {
            try
            {
                Exercise exercise = (Exercise)Enum.Parse(typeof(Exercise), exerciseType, true);
                return Ok(EntriesRepo.Inst.GetEntriesByExercise(exercise));
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
                EntriesRepo.Inst.AddEntry(entry);
                return HttpStatusCode.OK;
            }
        }
    }
}
