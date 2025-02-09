using run_script.Models;

namespace run_script.Repositories
{
    public interface IScriptRepository
    {
        Task<Script> GetScriptByIdAsync(int id);
        Task<List<Script>> GetAllScriptsAsync();
        Task<Script> CreateScriptAsync(Script script);
        Task<(string Output, string Error)> RunScriptAsync(string script);
        Task<(string Output, string Error)> RunScriptFromDatabaseAsync(int id);

    }
}
