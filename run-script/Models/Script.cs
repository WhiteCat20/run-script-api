using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace run_script.Models
{
    public class Script
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public string Name { get; set; }
        public string ScriptContent { get; set; }
        public int TimesAccessed { get; set; } = 0;
        public bool LifeStatus { get; set; } = false;
    }
}
