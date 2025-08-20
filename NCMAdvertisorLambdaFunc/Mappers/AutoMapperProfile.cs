using AutoMapper;
using NCMAdvertisorLambdaFunc.Dto;
using NCMAdvertisorLambdaFunc.Models;

namespace NCMAdvertisorLambdaFunc.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<KafkaMessage, AdvertisorEntity>();
        }
    }
}
