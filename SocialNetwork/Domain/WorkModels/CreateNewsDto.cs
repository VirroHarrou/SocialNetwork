using AutoMapper;
using SocialNetwork.Core.Common.Mappings;
using SocialNetwork.Core.News_.Commands.CreateNews;

namespace SocialNetwork.Domain.WorkModels
{
    public class CreateNewsDto : IMapWith<CreateNewsDto>
    {
        public string Topic { get; set; }
        public string MainText { get; set; }
        public string ImageUrl { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateNewsDto, CreateNewsCommand>()
                .ForMember(newsCommand => newsCommand.Topic,
                opt => opt.MapFrom(newsDto => newsDto.Topic))
                .ForMember(newsCommand => newsCommand.MainText,
                opt => opt.MapFrom(newsDto => newsDto.MainText))
                .ForMember(newsCommand => newsCommand.ImageUrl,
                opt => opt.MapFrom(newsDto => newsDto.ImageUrl));
        }
    }
}
