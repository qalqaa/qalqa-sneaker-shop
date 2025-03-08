using AutoMapper;
using Autorisation.Models;
using Autorisation.Data;

namespace Autorisation
{
    public class DataBaseMappings : Profile
    {
        public DataBaseMappings()
        {
            CreateMap<UserEntity, User>().ReverseMap();
        }
    }
}
