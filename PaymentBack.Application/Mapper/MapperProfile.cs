using AutoMapper;
using PaymentBack.Domain.Entities;
using PaymentBack.Domain.Models;
using PaymentBack.Entities;

namespace PaymentBack.Mapper
{
    public class MapperProfile: Profile
    {
        public MapperProfile() { 
        
            CreateMap<PaymentModel, PaymentEntity>()
                .ForMember(src => src.Id, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<PaymentCommonStatsModel, PaymentCommonStatsEntity>()
                .ReverseMap();

             CreateMap<PaymentGroupedByDayStatsModel, PaymentGroupedByDayStatsEntity>()
                .ReverseMap();
        }
    }
}
