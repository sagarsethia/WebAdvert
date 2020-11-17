using WebAdvertApi.Model;
namespace WebAdvertApi.Profile
{
    public class AdvertProfile:AutoMapper.Profile
    {
        public AdvertProfile()
        {
            CreateMap<AdvertModel,AdvertDBModel>();
        }
        
    }
}