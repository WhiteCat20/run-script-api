using AutoMapper;
using run_script.Models;

namespace run_script.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() {
            CreateMap<Script, ScriptDTO>().ReverseMap();
            CreateMap<Script, ScriptRequestDTO>().ReverseMap();

        }
    }
}
