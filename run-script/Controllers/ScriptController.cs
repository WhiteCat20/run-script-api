using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using run_script.Repositories;

namespace run_script.Controllers
{
    [Route("api/script")]
    [ApiController]
    public class ScriptController : ControllerBase
    {
        private readonly IScriptRepository repository;

        public ScriptController(IScriptRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllScripts()
        {
            var scripts = await repository.GetAllScriptsAsync();
            return Ok(scripts);
        }


        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetScriptById(int id)
        {
            var script = await repository.GetScriptByIdAsync(id);
            if (script == null)
            {
                return NotFound("Cannot get anything...");
            }
            return Ok(script);

        }

        [HttpPost("run")]
        public async Task<IActionResult> RunScript([FromBody] string script)
        {
            var (output, error) = await repository.RunScriptAsync(script);
            return Ok(new { Output = output, Error = error });
        }

        [HttpGet("run-from-db/{id}")]
        public async Task<IActionResult> RunScriptFromDb(int id)
        {
            var script = await repository.GetScriptByIdAsync(id);
            if (script == null)
            {
                return NotFound("Script not found.");
            }

            var (output, error) = await repository.RunScriptFromDatabaseAsync(id);

            return Ok(new
            {
                ScriptId = script.Id,
                ExecutedScript = script.ScriptContent,
                Output = output,
                Error = error,
                TimesAccessed = script.TimesAccessed // Menampilkan jumlah akses
            });
        }
    }
}
