using AutoMapper;
using NCMAdvertisorLambdaFunc.Model.Entities;
using NCMAdvertisorLambdaFunc.Model.Request;

namespace NCMAdvertisorLambdaFunc.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<KafkaMessage, AdvertisorEntity>();
        }
    }
}
