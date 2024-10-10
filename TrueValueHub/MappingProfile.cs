using AutoMapper;
using TrueValueHub.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Part, Part>()
            .ForMember(dest => dest.PartId, opt => opt.Ignore()); // Ignore PartId during mapping
        CreateMap<Manufacturing, Manufacturing>();
            /*.ForMember(dest => dest.ManufacturingId, opt => opt.Ignore()); */
    }
}
