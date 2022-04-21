namespace AutoParts.Infrastructure
{
    using AutoMapper;
    using AutoParts.Core.Models.Home;
    using AutoParts.Core.Models.Parts;
    using AutoParts.Infrastructure.Data.Models;

    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<PartDetailsServiceModel, PartFormModel>();
            this.CreateMap<Part, PartIndexViewModel>();

            this.CreateMap<Part, PartDetailsServiceModel>()
                .ForMember(p => p.UserId, cfg => cfg.MapFrom(p => p.Dealer.UserId));
        }
    }
}
