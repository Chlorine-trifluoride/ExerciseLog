using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
