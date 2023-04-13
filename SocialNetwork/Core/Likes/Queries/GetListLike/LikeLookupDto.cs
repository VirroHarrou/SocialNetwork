using AutoMapper;
using SocialNetwork.Core.Common.Mappings;
using SocialNetwork.Domain.Models;

namespace SocialNetwork.Core.Likes.Queries.GetListLike
{
    public class LikeLookupDto : IMapWith<Like>
    {
        public Guid UserId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Like, LikeLookupDto>()
                .ForMember(xDto => xDto.UserId,
                opt => opt.MapFrom(x => x.UserId));
        }
    }
}
