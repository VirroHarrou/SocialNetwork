using AutoMapper;
using SocialNetwork.Core.Common.Mappings;
using SocialNetwork.Core.News_.Commands.UpdateNews;

namespace SocialNetwork.Domain.WorkModels
{
    public class UpdateNewsDto : IMapWith<UpdateNewsCommand>
    {
        public Guid Id { get; set; }
        public string Topic { get; set; }
        public string MainText { get; set; }
        public string ImageUrl { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateNewsDto, UpdateNewsCommand>()
                .ForMember(x => x.MainText,
                o => o.MapFrom(x => x.MainText))
                .ForMember(x => x.Topic,
                o => o.MapFrom(x => x.Topic))
                .ForMember(x => x.ImageUrl,
                o => o.MapFrom(x => x.ImageUrl));
        }
    }
}
