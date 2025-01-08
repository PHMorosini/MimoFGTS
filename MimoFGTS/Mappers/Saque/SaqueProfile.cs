using AutoMapper;
using MimoFGTS.Content.Saque.DTO;
using MimoFGTS.Content.Saque.Entity;

namespace MimoFGTS.Mappers.Saque
{
    public class SaqueProfile : Profile
    {
        public SaqueProfile()
        {
            CreateMap<SaqueDTO, SaqueEntity>();
            CreateMap<SaqueEntity, SaqueDTO>();
        }
    }
}
