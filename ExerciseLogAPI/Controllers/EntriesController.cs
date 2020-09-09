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
