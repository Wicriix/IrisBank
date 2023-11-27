using AutoMapper;
using Iris.Data;
using Iris.Shared.OutputModel;

namespace Iris.Shared.Mappers
{
    public class MapperProfile : AutoMapper.Profile
    {
        public MapperProfile()
        {
            CreateMap<User, UserAuthOutputModel>().ReverseMap();
        }
    }
}
