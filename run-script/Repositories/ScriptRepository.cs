using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using run_script.Data;
using run_script.Models;

namespace run_script.Repositories
{
    public class ScriptRepository : IScriptRepository
    {
        private readonly ScriptDbContext context;

        public ScriptRepository(ScriptDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Script>> GetAllScriptsAsync()
        {
            return await context.Scripts.ToListAsync();
        }

        public async Task<Script> GetScriptByIdAsync(int id)
        {
            var scriptById = await context.Scripts.FindAsync(id);
            return scriptById;
        }

        public async Task<(string Output, string Error)> RunScriptAsync(string script)
        {
            return await ExecuteCommand(script);
        }

        public async Task<(string Output, string Error)> RunScriptFromDatabaseAsync(int id)
        {
            var scriptEntity = await GetScriptByIdAsync(id);
            if (scriptEntity == null)
            {
                return ("404", "Command not found.");
            }

            // Jalankan perintah
            var (output, error) = await ExecuteCommand(scriptEntity.ScriptContent);

            // Tingkatkan jumlah akses
            scriptEntity.TimesAccessed += 1;
            scriptEntity.LifeStatus = true;
            await context.SaveChangesAsync(); // Simpan perubahan ke database

            return (output, error);
        }

        private static async Task<(string, string)> ExecuteCommand(string command)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "wsl.exe", // Menggunakan WSL sebagai shell
                    Arguments = $"-e bash -c \"{command}\"", // Menjalankan perintah di dalam WSL
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using Process process = new Process { StartInfo = psi };
                process.Start();

                string output = await process.StandardOutput.ReadToEndAsync();
                string error = await process.StandardError.ReadToEndAsync();
                await process.WaitForExitAsync();

                return (output, error);
            }
            catch (Exception ex)
            {
                return ("", $"Error: {ex.Message}");
            }
        }

    }
}

