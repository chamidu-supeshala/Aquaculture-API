using Aquaculture.API.Data;
using Aquaculture.API.Dto;
using AutoMapper;

namespace Aquaculture.API.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            // Farm mappings
            CreateMap<Farm, FarmDto>().ForMember(dto => dto.Workers, opt => opt.MapFrom(x => x.Workers));
            CreateMap<FarmDto, Farm>().ForMember(obj => obj.Workers, opt => opt.Ignore());

            // Worker mappings
            CreateMap<Worker, WorkerDto>().ForMember(dto => dto.Farm, opt => opt.MapFrom(x => x.Farm));
            CreateMap<WorkerDto, Worker>().ForMember(obj => obj.Farm, opt => opt.Ignore());
        }
    }
}
